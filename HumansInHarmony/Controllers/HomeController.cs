using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HumansInHarmony.Models;

namespace HumansInHarmony.Controllers
{
    public class HomeController : Controller
    {
        public SongContext db = new SongContext();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult HomePage()
        {
            SongInfo song = ItunesDAL.FindSong();
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
        public IActionResult Result(User u)
        {
           // ViewBag.Name = u.Email;
            db.Add(u);
            db.SaveChanges();
            return View(u);
        }

        public IActionResult Result()
        {
            return View();
        }
    }
}
