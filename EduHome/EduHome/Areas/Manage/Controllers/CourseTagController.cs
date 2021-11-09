using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Manage.Controllers
{
    [Area("manage")]
    public class CourseTagController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CourseTagController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPageCount = Math.Ceiling(_context.Categories.Count() / 2d);

            List<CourseTag> courseTag = _context.CourseTags.Include(x => x.Course).Include(x => x.Tag).Skip((page - 1) * 2).Take(2).ToList();

            return View(courseTag);
        }

        public IActionResult Create()
        {
            ViewBag.Tags = _context.Tags.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name,
                Selected = true
            }).ToList();

            ViewBag.Courses = _context.Courses.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name,
                Selected = true
            }).ToList();

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(CourseTag courseTag)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.CourseTags.Add(courseTag);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}