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
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPageCount = Math.Ceiling(_context.Sliders.Count() / 2d);

            List<Slider> sliders = _context.Sliders.Skip((page - 1) * 2).Take(2).ToList();
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if (slider.ImageFile != null)
            {
                if (slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "The image type is incorrect!");
                    return View();
                }

                if (slider.ImageFile.Length > (1024 * 1024) * 2)
                {
                    ModelState.AddModelError("ImageFile", "The file size cannot exceed 2 mb!");
                    return View();
                }

                string rootPath = _env.WebRootPath;
                var filename = Guid.NewGuid().ToString() + slider.ImageFile.FileName;
                var path = Path.Combine(rootPath, "uploads", filename);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    slider.ImageFile.CopyTo(stream);
                }

                slider.Image = filename;
            }
            if (slider.BackImageFile != null)
            {
                if (slider.BackImageFile.ContentType != "image/png" && slider.BackImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("BackImageFile", "The image type is incorrect!");
                    return View();
                }

                if (slider.BackImageFile.Length > (1024 * 1024) * 2)
                {
                    ModelState.AddModelError("BackImageFile", "The file size cannot exceed 2 mb!");
                    return View();
                }

                string rootPath = _env.WebRootPath;
                var filename = Guid.NewGuid().ToString() + slider.BackImageFile.FileName;
                var path = Path.Combine(rootPath, "uploads", filename);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    slider.BackImageFile.CopyTo(stream);
                }

                slider.BackImage = filename;
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Sliders.Add(slider);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == id);

            if (slider == null) return RedirectToAction("index");

            return View(slider);
        }

        [HttpPost]
        public IActionResult Edit(int id, Slider slider)
        {
            if (!ModelState.IsValid) return View();
            Slider existSlider = _context.Sliders.FirstOrDefault(x => x.Id == id);

            if (slider.ImageFile != null)
            {
                if (slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "Mime type yanlisdir!");
                    return View();
                }

                if (slider.ImageFile.Length > (1024 * 1024) * 2)
                {
                    ModelState.AddModelError("ImageFile", "Faly olcusu 2MB-dan cox ola bilmez!");
                    return View();
                }

                string filename = Guid.NewGuid().ToString() + slider.ImageFile.FileName;
                string path = Path.Combine(_env.WebRootPath, "uploads", filename);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    slider.ImageFile.CopyTo(stream);
                }

                if (existSlider.Image != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads", existSlider.Image);
                    if (System.IO.File.Exists(existPath))
                    {
                        System.IO.File.Delete(existPath);
                    }
                }

                existSlider.Image = filename;
            }
            else if (slider.Image == null)
            {
                if (existSlider.Image != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads", existSlider.Image);
                    if (System.IO.File.Exists(existPath))
                    {
                        System.IO.File.Delete(existPath);
                    }

                    existSlider.Image = null;
                }
            }

            if (slider.BackImageFile != null)
            {
                if (slider.BackImageFile.ContentType != "image/png" && slider.BackImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("BackImageFile", "Mime type yanlisdir!");
                    return View();
                }

                if (slider.BackImageFile.Length > (1024 * 1024) * 2)
                {
                    ModelState.AddModelError("BackImageFile", "Faly olcusu 2MB-dan cox ola bilmez!");
                    return View();
                }

                string filename = Guid.NewGuid().ToString() + slider.BackImageFile.FileName;
                string path = Path.Combine(_env.WebRootPath, "uploads", filename);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    slider.BackImageFile.CopyTo(stream);
                }

                if (existSlider.BackImage != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads", existSlider.BackImage);
                    if (System.IO.File.Exists(existPath))
                    {
                        System.IO.File.Delete(existPath);
                    }
                }

                existSlider.BackImage = filename;
            }
            else if (slider.BackImage == null)
            {
                if (existSlider.BackImage != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads", existSlider.BackImage);
                    if (System.IO.File.Exists(existPath))
                    {
                        System.IO.File.Delete(existPath);
                    }
                    existSlider.BackImage = null;
                }
            }

            if (existSlider == null) return RedirectToAction("index");
            existSlider.Image = slider.Image;
            existSlider.Title = slider.Title;
            existSlider.TitleTwo = slider.TitleTwo;
            existSlider.Subtitle = slider.Subtitle;
            existSlider.Order = slider.Order;
            existSlider.BackImage = slider.BackImage;

            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == id);


            if (slider == null) return Json(new { isSucceded = false });

            _context.Sliders.Remove(slider);
            _context.SaveChanges();

            return Json(new { isSuccedded = true });
        }

    }
}
