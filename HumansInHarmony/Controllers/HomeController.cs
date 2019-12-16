using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HumansInHarmony.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;

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
            List<int> songIds = new List<int>(SongsArray.Songs);
            List<int> removeSongs = new List<int>();

            var currentUser = database.User.ToList().Find(u => u.Email == LoginController.UserEmail);

            var currentUserLikes = from likedSong in database.LikedSongs
                                   where likedSong.UserId == currentUser.Id
                                   select likedSong;
            var currentUserDislikes = from dislikedSong in database.DislikedSongs
                                      where dislikedSong.UserId == currentUser.Id
                                      select dislikedSong;

            foreach (var song in songIds)
            {
                foreach (var usersLikedSongs in currentUserLikes)
                {
                    if (usersLikedSongs.TrackId == song)
                    {
                        removeSongs.Add(song);
                    }
                }
                foreach (var usersDislikedSongs in currentUserDislikes)
                {
                    if (usersDislikedSongs.TrackId == song)
                    {
                        removeSongs.Add(song);
                    }
                }
            }
            foreach (var song in removeSongs)
            {
                songIds.Remove(song);
            }
            if (songIds.Count() > 0)
            {
                SongInfo currentSong = ItunesDAL.FindSong(songIds[0]);
                return View(currentSong);
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
        public IActionResult LikeSong(string trackId)
        {
            User currentUser = database.User.ToList().Find(u => u.Email == LoginController.UserEmail);

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
            User currentUser = database.User.ToList().Find(u => u.Email == LoginController.UserEmail);

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
            var users = database.User.ToList().Where(u => u.Email != LoginController.UserEmail);

            foreach (var u in users)
            {
                allUsers.Add(u);
            }
            return View(allUsers);
        }
        public IActionResult ProfilePage(string Email)
        {
            var findUser = database.User.ToList().Find(u => u.Email == Email);
            return View(findUser);
        }
        public IActionResult CompareLikes(int Id)
        {
            List<LikedSongs> MutualLikes = new List<LikedSongs>();

            var currentUser = database.User.ToList().Find(u => u.Email == LoginController.UserEmail);
            var comparedUser = database.User.ToList().Find(u => u.Id == Id);

            ViewBag.comparedUser = comparedUser.Name;
            ViewBag.comparedUserEmail = comparedUser.Email;

            var currentUserLikes = from likedsong in database.LikedSongs
                                   where likedsong.UserId == currentUser.Id
                                   select likedsong;

            var comparedUserLikes = from likedsong in database.LikedSongs
                                    where likedsong.UserId == Id
                                    select likedsong;

            foreach (LikedSongs song in currentUserLikes)
            {
                foreach (LikedSongs comparedSong in comparedUserLikes)
                {
                    if (song.TrackId == comparedSong.TrackId)
                    {
                        MutualLikes.Add(song);
                    }
                }
            }
            return View(MutualLikes);
        }
        public IActionResult CompareDislikes(int Id)
        {
            List<DislikedSongs> MutalDislikes = new List<DislikedSongs>();

            var currentUser = database.User.ToList().Find(u => u.Email == LoginController.UserEmail);
            var comparedUser = database.User.ToList().Find(u => u.Id == Id);

            ViewBag.comparedUser = comparedUser.Name;
            ViewBag.comparedUserEmail = comparedUser.Email;

            var currentUserLikes = from dislikeSongs in database.DislikedSongs
                                   where dislikeSongs.UserId == currentUser.Id
                                   select dislikeSongs;

            var comparedUserLikes = from dislikeSongs in database.DislikedSongs
                                    where dislikeSongs.UserId == Id
                                    select dislikeSongs;

            foreach (DislikedSongs song in currentUserLikes)
            {
                foreach (DislikedSongs comparedSong in comparedUserLikes)
                {
                    if (song.TrackId == comparedSong.TrackId)
                    {
                        MutalDislikes.Add(song);
                    }
                }
            }
            return View(MutalDislikes);
        }
        public IActionResult ComareLikesToDislikes(int Id)
        {
            List<LikedSongs> MutualSongs = new List<LikedSongs>();

            var currentUser = database.User.ToList().Find(u => u.Email == LoginController.UserEmail);
            var comparedUser = database.User.ToList().Find(u => u.Id == Id);

            ViewBag.comparedUser = comparedUser.Name;
            ViewBag.comparedUserEmail = comparedUser.Email;

            var currentUserLikes = from likedsongs in database.LikedSongs
                                   where likedsongs.UserId == currentUser.Id
                                   select likedsongs;

            var comparedUserDislikes = from dislokedSong in database.DislikedSongs
                                       where dislokedSong.UserId == Id
                                       select dislokedSong;

            foreach (var song in currentUserLikes)
            {
                foreach (var comparedSong in comparedUserDislikes)
                {
                    if (song.TrackId == comparedSong.TrackId)
                    {
                        MutualSongs.Add(song);
                    }
                }
            }
            return View(MutualSongs);
        }
    }
}
