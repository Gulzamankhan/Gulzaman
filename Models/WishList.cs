using System.Security.Cryptography;

namespace E_Commerce.Models
{
    public class WishList:SharedModel
    {
        public string Id { get; set; }
        public virtual AppUser Users { get; set; }
       
        public virtual ICollection<Product> Products { get; set;}
      
    }
}
