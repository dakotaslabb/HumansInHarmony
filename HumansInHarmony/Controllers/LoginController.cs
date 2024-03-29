﻿using System.Linq;
using HumansInHarmony.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HumansInHarmony.Controllers
{
    public class LoginController : Controller
    {
        HarmonyContext Database = new HarmonyContext();
        public static string UserEmail = "";
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User FormUser)
        {
            var output = Database.User.Count(u => u.Email == FormUser.Email);
            
            if (output == 1)
            {
                TempData["Error"] = "That user already exists.";
                return View();
            }
            else
            {
                HttpContext.Session.SetString("Email", FormUser.Email.ToString());
                UserEmail = HttpContext.Session.GetString("Email");
                TempData["Email"] = HttpContext.Session.GetString("Email");
                Database.Add(FormUser);
                Database.SaveChanges();
                return View(FormUser);
            }
        }
        public IActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserLogin(User FormUser)
        {
            User currentUser = Database.User.ToList().Find(u => u.Email == FormUser.Email);
            if (currentUser == null)
            {
                TempData["ReadError"] = "Invalid Email or Password.";
                return View();
            }
            else if (FormUser.Password == currentUser.Password)
            {
                HttpContext.Session.SetString("Email", FormUser.Email.ToString());
                UserEmail = HttpContext.Session.GetString("Email");
                TempData["Email"] = HttpContext.Session.GetString("Email");
                return View(currentUser);
            }
            else
            {
                return View();
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View();
        }
    }
}