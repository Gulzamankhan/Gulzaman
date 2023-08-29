using E_Commerce.Data;
using E_Commerce.Handler;
using E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace E_Commerce.Controllers
{
    //[Authorized(Roles =($"{Globalconfig.AdminRole},{Globalconfig.ShopkeeperRole}"))]
    [Authorized]
    public class ProductController : Controller
    {
        public readonly AppDbContext _dbContext;
        public ProductController(AppDbContext _context)
        {
            _dbContext = _context;
        }
    
        public IActionResult TastVuejs() { 
         return View();
        }
        public IActionResult Index()
        {
           
            return View();
        }
        public IActionResult Insert(Product model)
        {
            if (ModelState.IsValid)
            {
                if (model.UploudImages?.Any() == true)
                {
                    model.Images ??= new();
                    int imgrank = 0;
                    string basepath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot");
                    string apppath = Path.Combine("Images", "Products");
                    string directorypath = Path.Combine(basepath, apppath);
                    Directory.CreateDirectory(directorypath);
                    foreach (var item in model.UploudImages)
                    {
                        if (item.Length > 0)
                        {

                            string fileName = Path.GetRandomFileName().Replace(".", "") + Path.GetExtension(item.FileName);


                            using (var stream = new FileStream(Path.Combine(directorypath, fileName), FileMode.Create))
                            {

                                item.CopyTo(stream);
                                model.Images.Add(new Image
                                {
                                    Rank = ++imgrank,
                                    ProductId = model.Id,
                                    Path = Path.Combine(apppath, fileName).Replace("\\", "/")

                                });
                            }
                        }
                        
                    }
                    _dbContext.Products.Add(model);
                    int addrecord = _dbContext.SaveChanges();
                   if(addrecord > 0)
                    {
                        TempData["added"] = "added";
                    }
                }

            }

            return RedirectToAction("Select");
        }
     
        public IActionResult Select(string CategoryId)
        {
            var product= _dbContext.Products
                .Where(x=>string.IsNullOrEmpty(CategoryId)|| x.CategoryId==CategoryId)
               .Select(m=>new ProductViewmodel 
               {Name=m.Name
               ,Id=m.Id
               ,Description=m.Description
               ,EntryTime=m.EntryTime
               ,Price=m.Price
               ,Category=m.Category.Name
               ,Brand=m.Brand
               ,imageUrl=m.Images.OrderBy(x=>x.EntryTime).Select(m=>m.Path).FirstOrDefault()});
             
            return View(product);
        }
        public IActionResult Delete(string id)
        { 
           

            return View();
        
        }
       
       
        public IActionResult Edit(string id)
        {
            var product = _dbContext.Products
               .Include(x => x.Category)
               .Include(x => x.Images).FirstOrDefault(x => x.Id == id);
               

            if (product == null)
            {
                NotFound();
            }
            return View(product);
        }
        public IActionResult Save(Product model, List<string> deletedImagesIds)
        {
            if (ModelState.IsValid)
            {
                if (model.UploudImages?.Any() == true)
                {
                    List<Image> images =  new();
                    int imgrank = 0;
                    string basepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    string apppath = Path.Combine("Images", "Products");
                    string directorypath = Path.Combine(basepath, apppath);
                    Directory.CreateDirectory(directorypath);
                    foreach (var item in model.UploudImages)
                    {
                        if (item.Length > 0)
                        {

                            string fileName = Path.GetRandomFileName().Replace(".", "") + Path.GetExtension(item.FileName);


                            using (var stream = new FileStream(Path.Combine(directorypath, fileName), FileMode.Create))
                            {

                                item.CopyTo(stream);
                                images.Add(new Image
                                {
                                    Rank = ++imgrank,
                                    ProductId = model.Id,
                                    Path = Path.Combine(apppath, fileName).Replace("\\", "/")

                                });
                            }
                        }

                    }
                    var imgestodelete = _dbContext.Image.Where(m => m.ProductId == model.Id && deletedImagesIds.Contains(m.Id)).ToList();
                    _dbContext.AddRange(images);
                    var imgeUrltodelete=imgestodelete.Select(x=>x.Path).ToArray();
                    _dbContext.RemoveRange(imgestodelete);
                    _dbContext.Products.Update(model);
                   
                    int r = _dbContext.SaveChanges();
                    if(r>0)
                    {
                        foreach (var url in imgeUrltodelete)
                        {
                            try
                            {
                                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", url));
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                            
                        }
                        TempData["update"] = "updated";
                    }

                }

            }

            return RedirectToAction("Select");
        }

    }
}
