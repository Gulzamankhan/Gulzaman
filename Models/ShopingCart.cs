using NuGet.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class ShopingCart
    {

       
        public string Id { get; set; } = Path.GetRandomFileName().Replace(".", "");
        public string UserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        // Navigation property for the cart items
        public virtual List<CartItem> CartItems { get; set; }

    }
  

}
