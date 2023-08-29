using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Models;
using E_Commerce.Migrations;

namespace E_Commerce.Data
{
    public class AppDbContext : DbContext
    {
        private  IHttpContextAccessor _contextAccessor;
        public IHttpContextAccessor HttpContextAccessor { get; }
        private  AppUserviewModel Loggedinuser { get; set; }
        public AppDbContext (DbContextOptions<AppDbContext> options, IHttpContextAccessor contextAccessor)
            : base(options)
        {
            //_contextAccessor = contextAccessor;
            //proccesslogin();
            HttpContextAccessor= contextAccessor;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ShopingCart> Carts { get; set;}
        public DbSet<WishList> WishList { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<LoginHistory> LoginHistories { get; set; }
        public AppUserviewModel GetloggedinUser()
        {
            
            if (Loggedinuser != null) 
            { 
                return Loggedinuser;
            }
            var userid = HttpContextAccessor.HttpContext.Session.GetString(Globalconfig.LoginSessionName);
            var token= HttpContextAccessor.HttpContext.Request.Cookies[Globalconfig.LoginCookieName]?.ToString();
            if(string.IsNullOrEmpty(token) )
            {
                //andriod requst
                var tokens = HttpContextAccessor.HttpContext.Request.Headers[Globalconfig.LoginCookieName].ToString();
            }
            if (!string.IsNullOrEmpty(token))
            {
                var loginhistory = LoginHistories.Where(m => m.Token == token).FirstOrDefault();
                if (loginhistory == null || loginhistory.ValidTill < DateTime.Now)
                {
                    HttpContextAccessor.HttpContext.Response.Cookies.Delete(Globalconfig.LoginCookieName,new CookieOptions { IsEssential=true });
                    return null;
                }
                var user =LoginHistories.Include(x=>x.Appuser).Where(m=>m.Token==token)
                     .Select(x => new AppUserviewModel
                     {
                         Id = x.UserId,
                         Name = x.Appuser.Name,
                         Email = x.Appuser.Email,
                         Role = x.Appuser.Role.Select(n => n.Name).ToList()
                     }).FirstOrDefault();
                Loggedinuser=user;
                return Loggedinuser;
            }
            return null;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
         .Property(p => p.Price)
         .HasColumnType("decimal(18, 2)");
            // modelBuilder.Entity<AppUser>().HasOne(x=>x.ShoppingCart).WithOne(x=>x.AppUser).HasForeignKey<ShopingCart>(x=>x.UserId).OnDelete(DeleteBehavior.NoAction);   
            //modelBuilder.Entity<Address>().HasOne(x => x.Parent).WithMany(x => x.Child).HasForeignKey(x => x.Parent).OnDelete(DeleteBehavior.NoAction);
        }


    }
}
