using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HumansInHarmony.Models;
using System.Collections.Generic;

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
            List<SongInfo> songList = ItunesDAL.FindSong();
            return View(songList);
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
