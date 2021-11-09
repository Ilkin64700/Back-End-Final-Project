using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Manage.Controllers
{
    [Area("manage")]
    public class SpeakerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SpeakerController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPageCount = Math.Ceiling(_context.Speakers.Count() / 2d);

            List<Speaker> speakers = _context.Speakers.Skip((page - 1) * 2).Take(2).ToList();
            return View(speakers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Speaker speaker)
        {
            if (speaker.ImageFile != null)
            {
                if (speaker.ImageFile.ContentType != "image/png" && speaker.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "The image type is incorrect!");
                    return View();
                }

                if (speaker.ImageFile.Length > (1024 * 1024) * 2)
                {
                    ModelState.AddModelError("ImageFile", "The file size cannot exceed 2 mb!");
                    return View();
                }

                string rootPath = _env.WebRootPath;
                var filename = Guid.NewGuid().ToString() + speaker.ImageFile.FileName;
                var path = Path.Combine(rootPath, "uploads", filename);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    speaker.ImageFile.CopyTo(stream);
                }

                speaker.Image = filename;
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Speakers.Add(speaker);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Speaker speaker = _context.Speakers.FirstOrDefault(x => x.Id == id);

            if (speaker == null) return RedirectToAction("index");

            return View(speaker);
        }

        [HttpPost]
        public IActionResult Edit(int id, Speaker speaker)
        {
            if (!ModelState.IsValid) return View();

            Speaker existSpeaker = _context.Speakers.FirstOrDefault(x => x.Id == id);

            if (speaker.ImageFile != null)
            {
                if (speaker.ImageFile.ContentType != "image/png" && speaker.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "Mime type yanlisdir!");
                    return View();
                }

                if (speaker.ImageFile.Length > (1024 * 1024) * 2)
                {
                    ModelState.AddModelError("ImageFile", "Faly olcusu 2MB-dan cox ola bilmez!");
                    return View();
                }

                string filename = Guid.NewGuid().ToString() + speaker.ImageFile.FileName;
                string path = Path.Combine(_env.WebRootPath, "uploads", filename);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    speaker.ImageFile.CopyTo(stream);
                }

                if (existSpeaker.Image != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads", existSpeaker.Image);
                    if (System.IO.File.Exists(existPath))
                    {
                        System.IO.File.Delete(existPath);
                    }
                }

                existSpeaker.Image = filename;
            }
            else if (speaker.Image == null)
            {
                if (existSpeaker.Image != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads", existSpeaker.Image);
                    if (System.IO.File.Exists(existPath))
                    {
                        System.IO.File.Delete(existPath);
                    }

                    existSpeaker.Image = null;
                }
            }


            if (existSpeaker == null) return RedirectToAction("index");

            existSpeaker.Name = speaker.Name;
            existSpeaker.Position = speaker.Position;
            existSpeaker.Image = speaker.Image;

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Speaker speaker = _context.Speakers.FirstOrDefault(x => x.Id == id);

            if (speaker == null) return Json(new { isSucceded = false });

            _context.Speakers.Remove(speaker);

            _context.SaveChanges();

            return Json(new { isSuccedded = true });
        }
    }
}
