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
    public class PromotionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PromotionController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPageCount = Math.Ceiling(_context.Promotions.Count() / 2d);

            List<Promotion> promotions = _context.Promotions.Skip((page - 1) * 2).Take(2).ToList();
            return View(promotions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Promotion promotion)
        {
            if (promotion.ImageFile != null)
            {
                if (promotion.ImageFile.ContentType != "image/png" && promotion.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "The image type is incorrect!");
                    return View();
                }

                if (promotion.ImageFile.Length > (1024 * 1024) * 2)
                {
                    ModelState.AddModelError("ImageFile", "The file size cannot exceed 2 mb!");
                    return View();
                }

                string rootPath = _env.WebRootPath;
                var filename = Guid.NewGuid().ToString() + promotion.ImageFile.FileName;
                var path = Path.Combine(rootPath, "uploads", filename);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    promotion.ImageFile.CopyTo(stream);
                }

                promotion.Image = filename;
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Promotions.Add(promotion);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Promotion promotion = _context.Promotions.FirstOrDefault(x => x.Id == id);

            if (promotion == null) return RedirectToAction("index");

            return View(promotion);
        }

        [HttpPost]
        public IActionResult Edit(int id, Promotion promotion)
        {
            if (!ModelState.IsValid) return View();

            Promotion existPromotion = _context.Promotions.FirstOrDefault(x => x.Id == id);

            if (promotion.ImageFile != null)
            {
                if (promotion.ImageFile.ContentType != "image/png" && promotion.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "Mime type yanlisdir!");
                    return View();
                }

                if (promotion.ImageFile.Length > (1024 * 1024) * 2)
                {
                    ModelState.AddModelError("ImageFile", "Faly olcusu 2MB-dan cox ola bilmez!");
                    return View();
                }

                string filename = Guid.NewGuid().ToString() + promotion.ImageFile.FileName;
                string path = Path.Combine(_env.WebRootPath, "uploads", filename);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    promotion.ImageFile.CopyTo(stream);
                }

                if (existPromotion.Image != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads", existPromotion.Image);
                    if (System.IO.File.Exists(existPath))
                    {
                        System.IO.File.Delete(existPath);
                    }
                }

                existPromotion.Image = filename;
            }
            else if (promotion.Image == null)
            {
                if (existPromotion.Image != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads", existPromotion.Image);
                    if (System.IO.File.Exists(existPath))
                    {
                        System.IO.File.Delete(existPath);
                    }

                    existPromotion.Image = null;
                }
            }


            if (existPromotion == null) return RedirectToAction("index");

            existPromotion.Title = promotion.Title;
            existPromotion.Desc = promotion.Desc;
            existPromotion.Image = promotion.Image;

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Promotion promotion = _context.Promotions.FirstOrDefault(x => x.Id == id);


            if (promotion == null) return Json(new { isSucceded = false });

            _context.Promotions.Remove(promotion);
            _context.SaveChanges();

            return Json(new { isSuccedded = true });
        }
    }
}
