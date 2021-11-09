using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Manage.Controllers
{
    [Area("manage")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPageCount = Math.Ceiling(_context.Categories.Count() / 2d);

            List<Category> categories = _context.Categories.Include(x => x.Courses).Skip((page - 1) * 2).Take(2).ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (category == null) return RedirectToAction("index");

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(int id, Category category)
        {
            if (!ModelState.IsValid) return View();

            Category existCategory = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (existCategory == null) return RedirectToAction("index");

            existCategory.Name = category.Name;

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (category == null) return Json(new { isSucceded = false });

            _context.Categories.Remove(category);

            _context.SaveChanges();

            return Json(new { isSuccedded = true });
        }
    }
}