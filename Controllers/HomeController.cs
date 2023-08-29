
using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace PartialView.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext _appcontexte)
        {
            _context = _appcontexte;
        }
        public IActionResult Index()
        {
            //var username=   HttpContext.Session.GetString("username");
            //var password=HttpContext.Session.GetString("password");
            //    if(username == null || password == null)
            //   {
            //       return RedirectToAction("Index", "Login");
            //   }

            return View();
        }
        public IActionResult Client()
        {
            return View();
        }
        

    }
}
