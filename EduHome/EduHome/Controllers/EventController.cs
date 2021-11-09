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
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        public EventController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            EventViewModel eventVM = new EventViewModel
            {
                Events = _context.Events.Include(x => x.EventSpeakers).ToList(),
                Subscribes = _context.Subscribes.ToList(),
                Settings = _context.Settings.ToList()

            };
            return View(eventVM);
        }
    }
}
