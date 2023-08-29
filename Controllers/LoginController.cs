using E_Commerce.Data;
using E_Commerce.Handler;
using E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using NuGet.Protocol.Core.Types;
using System.Net;

namespace E_Commerce.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;
        public LoginController(AppDbContext apdbcontext)
        {
            _context = apdbcontext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserLogin(LoginviewModel model)
        {
            var user = _context.Users.Where(x => x.Email == model.Email).FirstOrDefault();
            if (user != null)
            {
                TempData["userlogin"] = "userlogin";
            }
            if (user == null)
            {

                ModelState.AddModelError("Email", "Email address not exist");
                return View(model);
            }
            if ((user.Id + model.Password).Encrypt() == user.EncryptePassword)
            {
                LoginHistory history = new LoginHistory()
                {
                    ClientInfo = _context.HttpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString(),
                    IpAddress = _context.HttpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                    UserId = user.Id,
                    ValidTill = model.RememberMe ? DateTime.Now.AddDays(7) : DateTime.Now.AddMinutes(20)
                };
                _context.Add(history);
                _context.SaveChanges();
                
                HttpContext.Session.SetString(Globalconfig.LoginSessionName, user.Id);
                // HttpContext.Session.SetString(Globalconfig.LoginSessionName, user.Id);
                HttpContext.Response.Cookies.Append(Globalconfig.LoginCookieName, history.Token,
                    new CookieOptions
                    {
                        IsEssential = true,
                        Expires = history.ValidTill
                    });
                return RedirectToAction("Client", "Home");
            }
            ModelState.AddModelError("", "invelid password");
            return View();
        }
        public IActionResult logout()
        {
            //HttpContext.Session.SetString("username", null);
            //HttpContext.Session.SetString("password", null);
            HttpContext.Session.Remove(Globalconfig.LoginSessionName);
            HttpContext.Session.Remove("password");
            return RedirectToAction("Index", "Home");

        }
    }
}
