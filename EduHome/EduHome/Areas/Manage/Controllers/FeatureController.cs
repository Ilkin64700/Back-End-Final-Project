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
    public class FeatureController : Controller
    {
        private readonly AppDbContext _context;
        public FeatureController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPageCount = Math.Ceiling(_context.Features.Count() / 2d);

            List<Feature> features = _context.Features.Skip((page - 1) * 2).Take(2).ToList();
            return View(features);
        }

        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Feature feature)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Features.Add(feature);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Feature feature = _context.Features.FirstOrDefault(x => x.Id == id);

            if (feature == null) return RedirectToAction("index");

            return View(feature);
        }


        [HttpPost]
        public IActionResult Edit(int id, Feature feature)
        {
            if (!ModelState.IsValid) return View();

            Feature existFeature = _context.Features.FirstOrDefault(x => x.Id == id);

            if (existFeature == null) return RedirectToAction("index");

            existFeature.Start = feature.Start;
            existFeature.Duration = feature.Duration;
            existFeature.ClassDuration = feature.ClassDuration;
            existFeature.SkillLevel = feature.SkillLevel;
            existFeature.Language = feature.Language;
            existFeature.Student = feature.Student;
            existFeature.Assesment = feature.Assesment;

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Feature feature = _context.Features.FirstOrDefault(x => x.Id == id);


            if (feature == null) return Json(new { isSucceded = false });

            _context.Features.Remove(feature);

            _context.SaveChanges();

            return Json(new { isSuccedded = true });
        }
    }
}
