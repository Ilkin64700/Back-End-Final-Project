using EduHome.DAL;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        public CourseController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            CourseViewModel courseVM = new CourseViewModel
            {
                Courses = _context.Courses.Include(x => x.CourseTags).Include(x => x.CourseImages).ToList(),
                Subscribes = _context.Subscribes.ToList(),
                Settings = _context.Settings.ToList(),
            };
            return View(courseVM);
        }
    }
}
