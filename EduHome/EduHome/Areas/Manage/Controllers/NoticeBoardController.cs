using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Manage.Controllers
{
    [Area("manage")]
    public class NoticeBoardController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public NoticeBoardController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPageCount = Math.Ceiling(_context.NoticeBoards.Count() / 2d);

            List<NoticeBoard> noticeBoards = _context.NoticeBoards.Skip((page - 1) * 2).Take(2).ToList();
            return View(noticeBoards);
        }

        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(NoticeBoard noticeBoard)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.NoticeBoards.Add(noticeBoard);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            NoticeBoard noticeBoard = _context.NoticeBoards.FirstOrDefault(x => x.Id == id);

            if (noticeBoard == null) return RedirectToAction("index");

            return View(noticeBoard);
        }

        [HttpPost]
        public IActionResult Edit(int id, NoticeBoard noticeBoard)
        {
            if (!ModelState.IsValid) return View();
            NoticeBoard existNoticeBoard = _context.NoticeBoards.FirstOrDefault(x => x.Id == id);
            if (existNoticeBoard == null) return RedirectToAction("index");

            existNoticeBoard.Date = noticeBoard.Date;
            existNoticeBoard.Desc = noticeBoard.Desc;

            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            NoticeBoard noticeBoard = _context.NoticeBoards.FirstOrDefault(x => x.Id == id);


            if (noticeBoard == null) return Json(new { isSucceded = false });

            _context.NoticeBoards.Remove(noticeBoard);

            _context.SaveChanges();

            return Json(new { isSuccedded = true });
        }
    }
}
