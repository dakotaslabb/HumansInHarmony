using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HumansInHarmony.Models;
using System.Linq;

namespace HumansInHarmony.Controllers
{
    public class HomeController : Controller
    {
        public readonly SongContext _context = new SongContext();
        public SongContext db = new SongContext();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult HomePage()
        {
            for (int i = 0; i < SongsArray.Songs.Length; i++)
            {
            SongInfo song = ItunesDAL.FindSong(i);
            return View(song);
            }
            return View();
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
        public IActionResult Register(User u)
        {
            TempData["UserName"] = u.Email;
            db.Add(u);
            db.SaveChanges();
            return View(u);
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
                TempData["UserName"] = FormUser.Email;
                ViewBag.Name = FormUser.Email;
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
            User u = new User();
            SongInfo song = ItunesDAL.SaveSong(SongId);
            u.Likes.Add(song);
            db.SaveChanges();
            return RedirectToAction("HomePage");
        }
        public IActionResult DislikeSong(string SongId)
        {
            User u = new User();
            SongInfo song = ItunesDAL.SaveSong(SongId);
            u.Dislikes.Add(song);
            db.SaveChanges();
            return RedirectToAction("HomePage");
        }
    }
}
