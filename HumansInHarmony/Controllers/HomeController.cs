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

        //public IActionResult LikeSong(string SongId)
        //{
        //    SongInfo song = ItunesDAL.SaveSong(SongId);
        //    Users.LikedSongs.Add(song);
        //    DB.Save();
        //    return RedirectToAction("HomePage");
        //}
        //public IActionResult DislikeSong(string SongId)
        //{
        //    SongInfo song = ItunesDAL.SaveSong(SongId);
        //    Users.DislikedSongs.Add(song);
        //    DB.Save();
        //    return RedirectToAction("HomePage");
        //}

    }
}
