using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using HumansInHarmony.Models;

namespace HumansInHarmony.Controllers
{
    public class HomeController : Controller
    {
        public User currentUser = new User();
        public SongContext db = new SongContext();
        private SongContext _context;
        public HomeController(SongContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult HomePage()
        {
            List<SongInfo> song = ItunesDAL.GetSongList();
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
        public ActionResult UserLogin(User user)
        {
            var account = _context.User.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();
            if (account != null)
            {
                HttpContext.Session.SetString("UserID", account.Email.ToString());
                HttpContext.Session.SetString("UserName", account.Name.ToString());
                TempData["Name"] = HttpContext.Session.GetString("Name");
                TempData["Email"] = HttpContext.Session.GetString("Email");
                TempData["UsePasswordr"] = HttpContext.Session.GetString("Password");
                return RedirectToAction("HomePage");
            }
            return View();
        }

        //[HttpPost]
        //public IActionResult UserLogin(User FormUser)
        //{
        //    User dbu = _context.User.ToList().Find(u => u.Email == FormUser.Email);

        //    if (dbu == null)
        //    {
        //        ViewBag.Error = "Invalide Email or Password";
        //        return View();
        //    }
        //    else if (FormUser.Password == dbu.Password)
        //    {
        //        this.currentUser = dbu;
        //        TempData["User"] = dbu.Name;
        //        return RedirectToAction("HomePage");
        //    }
        //    else
        //    {
        //        return RedirectToAction("UserLogin");
        //    }
        //}

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
            User u = new User(TempData.Peek("Name").ToString(), TempData.Peek("Email").ToString(), TempData.Peek("Password").ToString());
            SongInfo song = ItunesDAL.SaveSong(SongId);
            u.Dislikes.Add(song);
            db.SaveChanges();
            return RedirectToAction("HomePage");
        }
    }
}
