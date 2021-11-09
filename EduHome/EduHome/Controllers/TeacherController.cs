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
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        public TeacherController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            TeacherViewModel teacherVM = new TeacherViewModel
            {
                Settings = _context.Settings.ToList(),
                Teachers = _context.Teachers.Include(x => x.Courses).ToList(),
                Subscribes = _context.Subscribes.ToList(),
            };
            return View(teacherVM);
        }
    }
}
