using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Handler;

namespace E_Commerce.Controllers
{
    public class AcountController : Controller
    {
        private readonly AppDbContext _context;

        public AcountController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Acount
        public async Task<IActionResult> Index()
        {
              return View(await _context.Users.ToListAsync());
        }

        // GET: Acount/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // GET: Acount/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Acount/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,Password,ConfirmPassword")] AppUser appUser)
        {
            
            if (ModelState.IsValid)
            {
                var isexist = _context.Users.Select(x => x.Email == appUser.Email).FirstOrDefault(); 
                //var isexists = _context.Users.Include(x=>x.Adress).Where(x => x.Email == appUser.Email).Select(e=> new { Email= e.Email,UserAddressCodes= e.Adress.Select(x=>x.Code).ToList()}).FirstOrDefault();
                if(isexist==true)
                {
                    TempData["key"] = "Email already exist";
                    return RedirectToAction("Create");
                }
              
                appUser.EncryptePassword =(appUser.Id+ appUser.Password).Encrypt();
                _context.Add(appUser);
             var userid=await _context.SaveChangesAsync();
                if(userid>0)
                {
                    TempData["userregister"] = "user register successfull";
                }

                return RedirectToAction("Client","Home");
            }
            return View(appUser);
        }
        /// <summary>
        /// change password
        /// </summary>
       
        public IActionResult ChangePassword(AppUser model)
        {
            if (model.Password == model.ConfirmPassword)
            {
               var userid=  _context.GetloggedinUser().Id;
                var user = _context.Users.Where(x => x.Id ==userid).FirstOrDefault();
               
                user.EncryptePassword=  (userid+ model.Password).Encrypt();
                _context.SaveChanges();
                _context.HttpContextAccessor.HttpContext.Response.Cookies.Delete(Globalconfig.LoginCookieName, new CookieOptions { IsEssential=true});

                return RedirectToAction("Index", "Login");
              

            }
            ModelState.AddModelError("", "Both password not mached");
            return View(model);

        }
        [Authorized]
        public IActionResult ChangePassword()
        {

            return RedirectToAction();

        }


        // GET: Acount/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }
            return View(appUser);
        }

        // POST: Acount/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Email,Password,CNIC,Id,EntryTime")] AppUser appUser)
        {
            if (id != appUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppUserExists(appUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appUser);
        }

        // GET: Acount/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // POST: Acount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'AppDbContext.Users'  is null.");
            }
            var appUser = await _context.Users.FindAsync(id);
            if (appUser != null)
            {
                _context.Users.Remove(appUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserExists(string id)
        {
          return _context.Users.Any(e => e.Id == id);
        }
    }
}
