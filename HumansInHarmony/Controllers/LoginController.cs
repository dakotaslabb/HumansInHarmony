using System.Linq;
using HumansInHarmony.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HumansInHarmony.Controllers
{
    public class LoginController : Controller
    {
        public readonly SongContext _context = new SongContext();
        public SongContext db = new SongContext();
        [HttpPost]
        public IActionResult Register(User FormUser)
        {
            HttpContext.Session.SetString("Email", FormUser.Email.ToString());
            TempData["Email"] = HttpContext.Session.GetString("Email");
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
            User currentUser = _context.User.ToList().Find(u => u.Email == FormUser.Email);
            if (currentUser == null)
            {
                ViewBag.Error = "Invalide Email or Password";
                return View();
            }
            else if (FormUser.Password == currentUser.Password)
            {
                HttpContext.Session.SetString("Email", FormUser.Email.ToString());
                TempData["Email"] = HttpContext.Session.GetString("Email");
                return RedirectToAction("HomePage", "Home");
            }
            else
            {
                return RedirectToAction("UserLogin");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View();
        }

    }
}