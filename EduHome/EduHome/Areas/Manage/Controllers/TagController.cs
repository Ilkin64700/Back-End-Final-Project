using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Manage.Controllers
{
    [Area("manage")]

    public class TagController : Controller
    {
        private readonly AppDbContext _context;

        public TagController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPageCount = Math.Ceiling(_context.Tags.Count() / 2d);

            List<Tag> tags = _context.Tags.Skip((page - 1) * 2).Take(2).ToList();
            return View(tags);
        }

        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Tags.Add(tag);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Tag tag = _context.Tags.FirstOrDefault(x => x.Id == id);

            if (tag == null) return RedirectToAction("index");

            return View(tag);
        }

        [HttpPost]
        public IActionResult Edit(int id, Tag tag)
        {
            if (!ModelState.IsValid) return View();
            Tag existTag = _context.Tags.FirstOrDefault(x => x.Id == id);
            if (existTag == null) return RedirectToAction("index");

            existTag.Name = tag.Name;

            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Tag tag = _context.Tags.FirstOrDefault(x => x.Id == id);


            if (tag == null) return Json(new { isSucceded = false });

            _context.Tags.Remove(tag);
            _context.SaveChanges();

            return Json(new { isSuccedded = true });
        }
    }
}
