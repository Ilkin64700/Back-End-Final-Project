using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Manage.Controllers
{
    [Area("manage")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CourseController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPageCount = Math.Ceiling(_context.Courses.Count() / 2d);

            List<Course> courses = _context.Courses.Include(x => x.CourseTags).Include(x => x.CourseImages).Include(x => x.Category).Skip((page - 1) * 2).Take(2).ToList();
            return View(courses);
        }

        public IActionResult Create()
        {
            ViewBag.Teachers = _context.Teachers.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.courseTags = _context.CourseTags.ToList();

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Course course)
        {
            ViewBag.Teachers = _context.Teachers.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            //ViewBag.courseTags = _context.Tags.ToList();
            ViewBag.courseTags = _context.CourseTags.Include(x => x.Tag).Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Tag.Name,
                Selected = true
            }).ToList();

            if (course.ImageFile != null)
            {
                if (course.ImageFile.ContentType != "image/png" && course.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "The image type is incorrect!");
                    return View();
                }

                if (course.ImageFile.Length > (1024 * 1024) * 2)
                {
                    ModelState.AddModelError("ImageFile", "The file size cannot exceed 2 mb!");
                    return View();
                }

                string rootPath = _env.WebRootPath;
                var filename = Guid.NewGuid().ToString() + course.ImageFile.FileName;
                var path = Path.Combine(rootPath, "uploads", filename);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    course.ImageFile.CopyTo(stream);
                }

                course.Image = filename;
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (course.CourseTags != null)
            {
                foreach (var CourseTagId in course.CourseTags)
                {
                    CourseTag courseTag = new CourseTag
                    {
                        Course = course
                    };
                    _context.CourseTags.Add(courseTag);
                }
            }

            _context.Courses.Add(course);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Course course = _context.Courses.Include(x => x.CourseTags).FirstOrDefault(x => x.Id == id);

            if (course == null) return RedirectToAction("index");

            ViewBag.Teachers = _context.Teachers.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.CourseTags = _context.CourseTags.ToList();

            return View(course);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(int id, Course course)
        {
            ViewBag.Teachers = _context.Teachers.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.CourseTags = _context.CourseTags.ToList();

            Course existCourse = _context.Courses.Include(x => x.CourseTags).FirstOrDefault(x => x.Id == id);

            if (course.ImageFile != null)
            {
                if (course.ImageFile.ContentType != "image/png" && course.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "Mime type yanlisdir!");
                    return View();
                }

                if (course.ImageFile.Length > (1024 * 1024) * 2)
                {
                    ModelState.AddModelError("ImageFile", "Faly olcusu 2MB-dan cox ola bilmez!");
                    return View();
                }

                string filename = Guid.NewGuid().ToString() + course.ImageFile.FileName;
                string path = Path.Combine(_env.WebRootPath, "uploads", filename);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    course.ImageFile.CopyTo(stream);
                }

                if (existCourse.Image != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads", existCourse.Image);
                    if (System.IO.File.Exists(existPath))
                    {
                        System.IO.File.Delete(existPath);
                    }
                }

                existCourse.Image = filename;
            }
            else if (course.Image == null)
            {
                if (existCourse.Image != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads", existCourse.Image);
                    if (System.IO.File.Exists(existPath))
                    {
                        System.IO.File.Delete(existPath);
                    }

                    existCourse.Image = null;
                }
            }

            if (existCourse == null) return RedirectToAction("index");

            if (!_context.Teachers.Any(x => x.Id == course.TeacherId)) return RedirectToAction("index");

            if (!_context.Categories.Any(x => x.Id == course.CategoryId)) return RedirectToAction("index");

            existCourse.Name = course.Name;
            existCourse.Desc = course.Desc;
            existCourse.Price = course.Price;
            existCourse.AboutDesc = course.AboutDesc;
            existCourse.ApplyDesc = course.ApplyDesc;
            existCourse.CertificationDesc = course.CertificationDesc;
            existCourse.Price = course.Price;
            existCourse.CourseTags = course.CourseTags;
            existCourse.CourseImages = course.CourseImages;
            existCourse.CategoryId = course.CategoryId;
            existCourse.TeacherId = course.TeacherId;

            var existTags = _context.CourseTags.Where(x => x.CourseId == id).ToList();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Course course = _context.Courses.FirstOrDefault(x => x.Id == id);

            if (course == null) return RedirectToAction("index");

            _context.Courses.Remove(course);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
