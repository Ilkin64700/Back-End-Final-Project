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
    public class TestimonialController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public TestimonialController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPageCount = Math.Ceiling(_context.Testimonials.Count() / 2d);

            List<Testimonial> testimonials = _context.Testimonials.Skip((page - 1) * 2).Take(2).ToList();
            return View(testimonials);
        }

        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Testimonial testimonial)
        {
            if (testimonial.ImageFile != null)
            {
                if (testimonial.ImageFile.ContentType != "image/png" && testimonial.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "The image type is incorrect!");
                    return View();
                }

                if (testimonial.ImageFile.Length > (1024 * 1024) * 2)
                {
                    ModelState.AddModelError("ImageFile", "The file size cannot exceed 2 mb!");
                    return View();
                }

                string rootPath = _env.WebRootPath;
                var filename = Guid.NewGuid().ToString() + testimonial.ImageFile.FileName;
                var path = Path.Combine(rootPath, "uploads", filename);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    testimonial.ImageFile.CopyTo(stream);
                }

                testimonial.Image = filename;
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Testimonials.Add(testimonial);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Testimonial testimonial = _context.Testimonials.FirstOrDefault(x => x.Id == id);

            if (testimonial == null) return RedirectToAction("index");

            return View(testimonial);
        }

        [HttpPost]
        public IActionResult Edit(int id, Testimonial testimonial)
        {
            if (!ModelState.IsValid) return View();
            Testimonial existTestimonial = _context.Testimonials.FirstOrDefault(x => x.Id == id);

            if (testimonial.ImageFile != null)
            {
                if (testimonial.ImageFile.ContentType != "image/png" && testimonial.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "Mime type yanlisdir!");
                    return View();
                }

                if (testimonial.ImageFile.Length > (1024 * 1024) * 2)
                {
                    ModelState.AddModelError("ImageFile", "Faly olcusu 2MB-dan cox ola bilmez!");
                    return View();
                }

                string filename = Guid.NewGuid().ToString() + testimonial.ImageFile.FileName;
                string path = Path.Combine(_env.WebRootPath, "uploads", filename);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    testimonial.ImageFile.CopyTo(stream);
                }

                if (existTestimonial.Image != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads", existTestimonial.Image);
                    if (System.IO.File.Exists(existPath))
                    {
                        System.IO.File.Delete(existPath);
                    }
                }

                existTestimonial.Image = filename;
            }
            else if (testimonial.Image == null)
            {
                if (existTestimonial.Image != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads", existTestimonial.Image);
                    if (System.IO.File.Exists(existPath))
                    {
                        System.IO.File.Delete(existPath);
                    }

                    existTestimonial.Image = null;
                }
            }

            if (existTestimonial == null) return RedirectToAction("index");
            existTestimonial.Image = testimonial.Image;
            existTestimonial.Title = testimonial.Title;
            existTestimonial.Name = testimonial.Name;
            existTestimonial.Position = testimonial.Position;
            existTestimonial.Order = testimonial.Order;

            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Testimonial testimonial = _context.Testimonials.FirstOrDefault(x => x.Id == id);

            if (testimonial == null) return Json(new { isSucceded = false });

            _context.Testimonials.Remove(testimonial);

            _context.SaveChanges();

            return Json(new { isSuccedded = true });
        }

    }
}
