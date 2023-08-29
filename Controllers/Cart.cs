using E_Commerce.Data;
using E_Commerce.Handler;
using E_Commerce.Migrations;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    [Authorized]
    public class Cart : Controller
    {
        private AppDbContext _context;
        private string loggedInUserId;
        public Cart(AppDbContext context)
        {
            _context = context;
            loggedInUserId = _context.GetloggedinUser().Id;
        }
        [Authorized]
        public IActionResult Index()
        {
            return View();
        }
        //add update
        [HttpPost]
        public IActionResult DeleteItem(string id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return Json(new { Status = false, Msg = "Product not found for id " + id });
            }

            var cart = _context.Carts.Include(m => m.CartItems).Where(m => m.UserId == loggedInUserId).FirstOrDefault();

            var existingItem = cart.CartItems.Where(m => m.ProductId == id).FirstOrDefault();
            if (existingItem != null)
            {
                _context.Remove(existingItem);
            }
            _context.SaveChanges();
            return GetCartItems();
        }

        //end
        public IActionResult AddOrUpdateCart(string id,int qyt,bool isupdate=false)
        {
          var products=  _context.Products.FirstOrDefault(p=>p.Id==id);
            if(products==null)
            {
                return Json(new { Status = false, Msg = "Product Not found"+id }); 
            }
            var cart = _context.Carts.Include(m=>m.CartItems).Where(m => m.UserId == loggedInUserId).FirstOrDefault(); 
            if(cart==null)
            {
                cart = new ShopingCart { UserId = loggedInUserId,CartItems=new() };
                _context.Carts.Add(cart);
                _context.SaveChanges();
            }
            var existingitem= cart.CartItems.Where(x => x.ProductId == id).FirstOrDefault();
            if(existingitem != null)
            {
                if (isupdate) existingitem.Quantity = qyt;
               else existingitem.Quantity = qyt;
            }
            else
            {
               CartItem currentitem=new() { Quantity = qyt, ProductId=id,ShoppingCartId=cart.Id };
                cart.CartItems.Add(currentitem);
                _context.Add(currentitem);
                _context.SaveChanges();
            }
             var productids= cart.CartItems.Select(m=>m.ProductId).ToList();
            var product = _context.Products.Where(x => productids.Contains(x.Id))
                 .Select(m => new
                 {
                     m.Id,
                     m.Name,
                   Imageurl= m.Images.OrderBy(x => x.Rank).Select(u => u.Path).FirstOrDefault(),
                   m.Price,
                 categoryName=  m.Category.Name,
                 }).ToList();
         var result= product.Select(m => new
            {
                m.Id,
                m.Name,
                m.Price,
                m.categoryName,
                m.Imageurl,
              qty=cart.CartItems.Where(c=>c.ProductId==m.Id).Select(q=>q.Quantity).FirstOrDefault(),
            });
            return Json(new { status = true, data = result });
        }

        [HttpPost]
        public IActionResult GetCartItems()
        {


            
            System.Threading.Thread.Sleep(250);
            if (string.IsNullOrEmpty(loggedInUserId))
            {
                return Json(new { Status = false, Msg = "Log in requried." });
            }

            var cart = _context.Carts.Include(m => m.CartItems).Where(m => m.UserId == loggedInUserId).FirstOrDefault();
            if (cart == null)
            {
                cart = new ShopingCart { UserId = loggedInUserId, CartItems = new() };
                _context.Carts.Add(cart);
                _context.SaveChanges();
            }


            var productIds = cart.CartItems.Select(m => m.ProductId).ToList();
            var products = _context.Products.Where(m => productIds.Contains(m.Id))
                .Select(m => new
                {
                    ProductId = m.Id,
                    m.Name,
                    ImageUrl = m.Images.OrderBy(s => s.Rank).Select(s => s.Path).FirstOrDefault(),
                    m.Price,
                    CategoryName = m.Category.Name
                }).ToList();

            var result = products.Select(m => new
            {
                m.ProductId,
                m.Name,
                m.CategoryName,
                m.Price,
                m.ImageUrl,
                Qty = cart.CartItems.Where(i => i.ProductId == m.ProductId).Select(q => q.Quantity).FirstOrDefault()
            }).ToList();

            return Json(new { Status = true, Data = result });
        }



    }
}

