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
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        public AboutController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            AboutViewModel aboutVM = new AboutViewModel
            {
                Promotions = _context.Promotions.ToList(),
                Testimonials = _context.Testimonials.ToList(),
                Settings = _context.Settings.ToList(),
                Teachers = _context.Teachers.Include(x => x.Courses).Take(4).ToList(),
                Subscribes = _context.Subscribes.ToList(),
                NoticeBoards = _context.NoticeBoards.ToList(),
            };
            return View(aboutVM);
        }
    }
}
