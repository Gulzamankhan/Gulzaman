using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _dbcontext;
        public CategoryController(AppDbContext context)
        {
            _dbcontext = context;
        }
        public IActionResult Index(bool iar)
        {
            if(iar)
            {
                System.Threading.Thread.Sleep(1500);
                return PartialView();
            }
            return View();
        }
           
        public IActionResult Insert(Category model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImgeLogo != null && model.ImgeLogo.Length > 0)
                {
                    string basepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    string apppath = Path.Combine("Images", "Categories");
                    string fileName = Path.GetRandomFileName().Replace(".", "") + Path.GetExtension(model.ImgeLogo.FileName);

                    string directorypath = Path.Combine(basepath, apppath);
                    Directory.CreateDirectory(directorypath);
                    using (var stream = new FileStream(Path.Combine(directorypath, fileName), FileMode.Create))
                    {

                        model.ImgeLogo.CopyTo(stream);
                    }

                    model.Logo = Path.Combine(apppath, fileName).Replace("\\", "/");

                }
                _dbcontext.Categories.Add(model);
             int add=   _dbcontext.SaveChanges();
              if(add > 0)
                {
                    TempData["Added"] = "Added";
                }

            }

            return RedirectToAction("Select");
        }
        public IActionResult Select(string k ,CategoryType? type)
        {
            if(type== null)
            {
                type = CategoryType.Category;
                
            }
            ViewBag.type = type;
            var queribale = _dbcontext.Categories.Where(m=>m.Type==type);
            if (!string.IsNullOrEmpty(k))
            {
                queribale = queribale.Where(m => m.Name.Contains(k));

            }

            ViewBag.searchurl = "/Category/Select";

           // ViewBag.searchbykey = k;
            var obj = queribale.ToList();
            
            
            return View(obj);
        }
        public IActionResult Get(string id)
        {
            var category = _dbcontext.Categories.Where(x => x.Id == id).FirstOrDefault();
            if (category == null)
            {
                NotFound();
            }

            return View(category);
        }
        public IActionResult Delete(string id)
        {
            var category = _dbcontext.Categories.Where(x => x.Id == id).FirstOrDefault();
            _dbcontext.Categories.Remove(category);
           var isdelete= _dbcontext.SaveChanges();
            if(isdelete > 0) 
            {
                TempData["delete"] = "deleted";
            }    
            return RedirectToAction("Select");

        }
        public IActionResult Details(string id)
        {
            var category = _dbcontext.Categories.Where(x => x.Id == id).FirstOrDefault();

            return View(category);
        }
        public IActionResult Edit(string id)
        {
            var category = _dbcontext.Categories.Where(x => x.Id == id).FirstOrDefault();  
            return View(category);
        }
        public IActionResult Save(Category model)
        {
            if (model.Id != null)
            {
                string basepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                string apppath = Path.Combine("Images", "Categories");
                string fileName = Path.GetRandomFileName().Replace(".", "") + Path.GetExtension(model.ImgeLogo.FileName);

                string directorypath = Path.Combine(basepath, apppath);
                Directory.CreateDirectory(directorypath);
                using (var stream = new FileStream(Path.Combine(directorypath, fileName), FileMode.Create))
                {

                    model.ImgeLogo.CopyTo(stream);
                }

                model.Logo = Path.Combine(apppath, fileName).Replace("\\", "/");

            }
            _dbcontext.Categories.Update(model);
            var update = _dbcontext.SaveChanges();
            if (update > 0)
            {
                TempData["update"] = "update";
            }
            return RedirectToAction("Select");
        }
    }
}
