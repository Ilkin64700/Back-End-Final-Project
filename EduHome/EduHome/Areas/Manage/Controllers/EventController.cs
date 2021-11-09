using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Manage.Controllers
{
    [Area("manage")]

    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public EventController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPageCount = Math.Ceiling(_context.Events.Count() / 2d);

            List<Event> events = _context.Events.Include(x => x.EventSpeakers).Skip((page - 1) * 2).Take(2).ToList();
            return View(events);
        }

        public IActionResult Create()
        {
            ViewBag.Speakers = _context.Speakers.ToList();
            ViewBag.Categories = _context.Categories.ToList();

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Event _event)
        {
            
            ViewBag.Speakers = _context.Speakers.ToList();
            ViewBag.Categories = _context.Categories.ToList();

            if (!_context.Speakers.Any(x => x.Id == _event.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Xeta var!");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Events.Add(_event);
            _context.SaveChanges();


            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Event _event = _context.Events.Include(x => x.EventSpeakers).FirstOrDefault(x => x.Id == id);

            if (_event == null) return RedirectToAction("index");

            ViewBag.Speakers = _context.Speakers.ToList();
            ViewBag.Categories = _context.Categories.ToList();

            return View(_event);
        }

        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public IActionResult Edit(int id, Event _event)
        //{
            
        //    ViewBag.Speakers = _context.Speakers.ToList();
        //    ViewBag.Categories = _context.Categories.ToList();

        //    Course existCourse = _context.Courses.Include(x => x.CourseTags).FirstOrDefault(x => x.Id == id);

        //    if (_event.ImageFile != null)
        //    {
        //        if (_event.ImageFile.ContentType != "image/png" && _event.ImageFile.ContentType != "image/jpeg")
        //        {
        //            ModelState.AddModelError("ImageFile", "Mime type yanlisdir!");
        //            return View();
        //        }

        //        if (_event.ImageFile.Length > (1024 * 1024) * 2)
        //        {
        //            ModelState.AddModelError("ImageFile", "Faly olcusu 2MB-dan cox ola bilmez!");
        //            return View();
        //        }

        //        string filename = Guid.NewGuid().ToString() + _event.ImageFile.FileName;
        //        string path = Path.Combine(_env.WebRootPath, "uploads", filename);

        //        using (FileStream stream = new FileStream(path, FileMode.Create))
        //        {
        //            _event.ImageFile.CopyTo(stream);
        //        }

        //        if (existCourse.Image != null)
        //        {
        //            string existPath = Path.Combine(_env.WebRootPath, "uploads", existCourse.Image);
        //            if (System.IO.File.Exists(existPath))
        //            {
        //                System.IO.File.Delete(existPath);
        //            }
        //        }

        //        existCourse.Image = filename;
        //    }
        //    else if (_event.Image == null)
        //    {
        //        if (existCourse.Image != null)
        //        {
        //            string existPath = Path.Combine(_env.WebRootPath, "uploads", existCourse.Image);
        //            if (System.IO.File.Exists(existPath))
        //            {
        //                System.IO.File.Delete(existPath);
        //            }

        //            existCourse.Image = null;
        //        }
        //    }

        //    if (existCourse == null) return RedirectToAction("index");

        //    if (!_context.Speakers.Any(x => x.Id == _event.SpeakerId)) return RedirectToAction("index");

        //    if (!_context.Categories.Any(x => x.Id == _event.CategoryId)) return RedirectToAction("index");

        //    existEvent.Name = _event.Name;
        //    existCourse.Desc = _event.Desc;
        //    existCourse.Price = _event.Price;
        //    existCourse.AboutDesc = _event.AboutDesc;
        //    existCourse.ApplyDesc = _event.ApplyDesc;
        //    existCourse.CertificationDesc = _event.CertificationDesc;
        //    existCourse.Price = _event.Price;
        //    existCourse.CourseTags = _event.CourseTags;
        //    existCourse.CourseImages = _event.CourseImages;
        //    existCourse.CategoryId = _event.CategoryId;
        //    existCourse.TeacherId = _event.TeacherId;

        //    return RedirectToAction("index");
        //}

        public IActionResult Delete(int id)
        {
            Event _event = _context.Events.FirstOrDefault(x => x.Id == id);

            if (_event == null) return Json(new { isSucceded = false });

            _context.Events.Remove(_event);

            _context.SaveChanges();

            return Json(new { isSuccedded = true });
        }
    }
}
