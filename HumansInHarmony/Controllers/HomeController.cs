using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HumansInHarmony.Models;
using System.Linq;
using System.Collections.Generic;

namespace HumansInHarmony.Controllers
{
    public class HomeController : Controller
    {
        public User currentUser;
        public readonly SongContext _context = new SongContext();
        public SongContext db = new SongContext();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult HomePage()
        {
            List<SongInfo> song = ItunesDAL.FindSong();
            return View(song);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Register(User FormUser)
        {
            currentUser = FormUser;
            TempData["User"] = FormUser.Name;
            db.Add(FormUser);
            db.SaveChanges();
            return View(FormUser);
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserLogin(User FormUser)
        {
            User dbu = _context.User.ToList().Find(u => u.Email == FormUser.Email);

            if (dbu == null)
            {
                ViewBag.Error = "Invalide Email or Password";
                return View();
            }
            else if (FormUser.Password == dbu.Password)
            {
                currentUser = FormUser;
                TempData["User"] = FormUser.Name;
                return RedirectToAction("HomePage");
            }
            else
            {
                return RedirectToAction("UserLogin");
            }
        }

        public IActionResult Logout()
        {
            return View();
        }
        public IActionResult LikeSong(string SongId)
        {
            
            SongInfo song = ItunesDAL.SaveSong(SongId);
            currentUser.Likes.Add(song);
            db.SaveChanges();
            return RedirectToAction("HomePage");
        }
        public IActionResult DislikeSong(string SongId)
        {
            SongInfo song = ItunesDAL.SaveSong(SongId);
            currentUser.Dislikes.Add(song);
            db.SaveChanges();
            return RedirectToAction("HomePage");
        }
    }
}
