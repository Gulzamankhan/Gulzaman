using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class RemoteController : Controller
    {
        private readonly AppDbContext _dbcontext;
        public RemoteController(AppDbContext _context)
        {
                _dbcontext= _context;
        }
        public IActionResult Index(Category model)
        {
            var isExist=_dbcontext.Categories.Where(m=>m.Name==model.Name).Any();
            return Json(!isExist);
        }
    }
}
