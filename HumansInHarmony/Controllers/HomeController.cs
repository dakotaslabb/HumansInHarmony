using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HumansInHarmony.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;

namespace HumansInHarmony.Controllers
{
    public class HomeController : Controller
    {
        public SongContext database = new SongContext();

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

        public IActionResult LikeSong(string trackId)
        {
            string email = HttpContext.Session.GetString("Email");
            User currentUser = database.User.ToList().Find(u => u.Email == email);

            LikedSongs song = ItunesDAL.SaveLike(trackId);
            currentUser.Likes.Add(song);

            var saved = false;
            while (!saved)
            {
                try
                {
                    // Attempt to save changes to the database
                    database.User.Update(currentUser).State = EntityState.Modified;
                    database.SaveChanges();
                    saved = true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is User)
                        {
                            var proposedValues = entry.CurrentValues;
                            var databaseValues = entry.GetDatabaseValues();

                            foreach (var property in proposedValues.Properties)
                            {
                                var proposedValue = proposedValues[property];
                                var databaseValue = databaseValues[property];

                                // TODO: decide which value should be written to database
                                // proposedValues[property] = <value to be saved>;
                            }

                            // Refresh original values to bypass next concurrency check
                            entry.OriginalValues.SetValues(databaseValues);
                        }
                        else
                        {
                            throw new NotSupportedException(
                                "Don't know how to handle concurrency conflicts for "
                                + entry.Metadata.Name);
                        }
                    }
                }
            }
            return RedirectToAction("HomePage");
        }

        public IActionResult DislikeSong(string trackId)
        {
            string email = HttpContext.Session.GetString("Email");
            User currentUser = database.User.ToList().Find(u => u.Email == email);

            DislikedSongs song = ItunesDAL.SaveDislike(trackId);
            currentUser.Dislikes.Add(song);

            var saved = false;
            while (!saved)
            {
                try
                {
                    // Attempt to save changes to the database
                    database.User.Update(currentUser).State = EntityState.Modified;
                    database.SaveChanges();
                    saved = true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is User)
                        {
                            var proposedValues = entry.CurrentValues;
                            var databaseValues = entry.GetDatabaseValues();

                            foreach (var property in proposedValues.Properties)
                            {
                                var proposedValue = proposedValues[property];
                                var databaseValue = databaseValues[property];

                                // TODO: decide which value should be written to database
                                // proposedValues[property] = <value to be saved>;
                            }

                            // Refresh original values to bypass next concurrency check
                            entry.OriginalValues.SetValues(databaseValues);
                        }
                        else
                        {
                            throw new NotSupportedException(
                                "Don't know how to handle concurrency conflicts for "
                                + entry.Metadata.Name);
                        }
                    }
                }
            }
            return RedirectToAction("HomePage");
        }

        public IActionResult AllUsers()
        {
            List<User> allUsers = new List<User>();
            string email = HttpContext.Session.GetString("Email");
            var users  = database.User.ToList().Where(u => u.Email != email);

            foreach (var u in users)
            {
                allUsers.Add(u);
            }
            return View(allUsers);
        }

        public IActionResult Matches(int Id)
        {
            List<LikedSongs> MutualLikes = new List<LikedSongs>();

            string email = HttpContext.Session.GetString("Email");
            var currentUser = database.User.ToList().Find(u => u.Email == email);
            var comparedUser = database.User.ToList().Find(u => u.Id == Id);

            foreach (var song in currentUser.Likes)
            {
                foreach (var comparedSong in comparedUser.Likes)
                {
                    if (song.Id == comparedSong.Id)
                    {
                        MutualLikes.Add(song);
                    }

                }
            }
            return View(MutualLikes);
        }
    }
}
