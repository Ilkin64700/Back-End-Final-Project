using EduHome.DAL;
using EduHome.Models;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeViewModel homeVM = new HomeViewModel
            {
                Sliders = _context.Sliders.OrderBy(x => x.Order).ToList(),
                Courses = _context.Courses.Include(x => x.CourseTags).Include(x => x.CourseImages).Take(3).ToList(),
                Events = _context.Events.Include(x => x.EventSpeakers).ToList(),
                Promotions = _context.Promotions.ToList(),
                Testimonials = _context.Testimonials.ToList(),
                Settings = _context.Settings.ToList(),
                NoticeBoards = _context.NoticeBoards.ToList(),
                Subscribes = _context.Subscribes.ToList(),
            };
            return View(homeVM);
        }
    }
}