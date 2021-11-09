using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Manage.Controllers
{
    [Area("manage")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SettingController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPageCount = Math.Ceiling(_context.Settings.Count() / 2d);

            List<Setting> settings = _context.Settings.ToList();
            return View(settings);
        }

        public IActionResult Edit(int id)
        {
            Setting setting = _context.Settings.FirstOrDefault(x => x.Id == id);

            if (setting == null) return RedirectToAction("index");

            return View(setting);
        }


        [HttpPost]
        public IActionResult Edit(int id, Setting setting)
        {
            if (!ModelState.IsValid) return View();
            Setting existSetting = _context.Settings.FirstOrDefault(x => x.Id == id);

            if (setting.Logofile != null)
            {
                if (setting.Logofile.ContentType != "image/png" && setting.Logofile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("Logofile", "Mime type yanlisdir!");
                    return View();
                }

                if (setting.Logofile.Length > (1024 * 1024) * 2)
                {
                    ModelState.AddModelError("Logofile", "Faly olcusu 2MB-dan cox ola bilmez!");
                    return View();
                }

                string filename = Guid.NewGuid().ToString() + setting.Logofile.FileName;
                string path = Path.Combine(_env.WebRootPath, "uploads", filename);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    setting.Logofile.CopyTo(stream);
                }

                if (existSetting.Logo != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads", existSetting.Logo);
                    if (System.IO.File.Exists(existPath))
                    {
                        System.IO.File.Delete(existPath);
                    }
                }

                existSetting.Logo = filename;
            }
            else if (setting.Logo == null)
            {
                if (existSetting.Logo != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads", existSetting.Logo);
                    if (System.IO.File.Exists(existPath))
                    {
                        System.IO.File.Delete(existPath);
                    }

                    existSetting.Logo = null;
                }
            }

            if (setting.FooterLogofile != null)
            {
                if (setting.FooterLogofile.ContentType != "image/png" && setting.FooterLogofile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("FooterLogofile", "Mime type yanlisdir!");
                    return View();
                }

                if (setting.FooterLogofile.Length > (1024 * 1024) * 2)
                {
                    ModelState.AddModelError("FooterLogofile", "Faly olcusu 2MB-dan cox ola bilmez!");
                    return View();
                }

                string filename = Guid.NewGuid().ToString() + setting.FooterLogofile.FileName;
                string path = Path.Combine(_env.WebRootPath, "uploads", filename);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    setting.FooterLogofile.CopyTo(stream);
                }

                if (existSetting.FooterLogo != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads", existSetting.FooterLogo);
                    if (System.IO.File.Exists(existPath))
                    {
                        System.IO.File.Delete(existPath);
                    }
                }

                existSetting.FooterLogo = filename;
            }
            else if (setting.FooterLogo == null)
            {
                if (existSetting.FooterLogo != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads", existSetting.FooterLogo);
                    if (System.IO.File.Exists(existPath))
                    {
                        System.IO.File.Delete(existPath);
                    }

                    existSetting.FooterLogo = null;
                }
            }

            if (setting.Bannerphotofile != null)
            {
                if (setting.Bannerphotofile.ContentType != "image/png" && setting.Bannerphotofile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("Bannerphotofile", "Mime type yanlisdir!");
                    return View();
                }

                if (setting.Bannerphotofile.Length > (1024 * 1024) * 2)
                {
                    ModelState.AddModelError("Bannerphotofile", "Faly olcusu 2MB-dan cox ola bilmez!");
                    return View();
                }

                string filename = Guid.NewGuid().ToString() + setting.Bannerphotofile.FileName;
                string path = Path.Combine(_env.WebRootPath, "uploads", filename);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    setting.Bannerphotofile.CopyTo(stream);
                }

                if (existSetting.Bannerphotofile != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads", existSetting.Bannerphoto);
                    if (System.IO.File.Exists(existPath))
                    {
                        System.IO.File.Delete(existPath);
                    }
                }

                existSetting.Bannerphoto = filename;
            }
            else if (setting.Bannerphoto == null)
            {
                if (existSetting.Bannerphoto != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads", existSetting.Bannerphoto);
                    if (System.IO.File.Exists(existPath))
                    {
                        System.IO.File.Delete(existPath);
                    }

                    existSetting.Bannerphoto = null;
                }
            }

            if (existSetting == null) return RedirectToAction("index");

            existSetting.Logo = setting.Logo;
            existSetting.Question = setting.Question;
            existSetting.Phone = setting.Phone;
            existSetting.Phone2 = setting.Phone2;
            existSetting.Phone3 = setting.Phone3;
            existSetting.FooterDesc = setting.FooterDesc;
            existSetting.Adress = setting.Adress;
            existSetting.InstagramURL = setting.InstagramURL;
            existSetting.PinterestURL = setting.PinterestURL;
            existSetting.TwitterURL = setting.TwitterURL;
            existSetting.Bannerphoto = setting.Bannerphoto;
            existSetting.FooterLogo = setting.FooterLogo;

            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
