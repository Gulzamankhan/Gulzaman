
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class AppUser:SharedModel
    {
        [Required(ErrorMessage ="Name is Required")]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required(ErrorMessage ="Email Is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string EncryptePassword { get; set; }
        
        [StringLength(30)]
        [DataType(DataType.Password)]
        [NotMapped]
        public string Password { get; set; }
        [Compare("Password")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
       // public virtual ShopingCart ShoppingCart { get; set; }
        public virtual ICollection<Role> Role { get; set; }
        public virtual ICollection<WishList> WishList { get; set; }
        public virtual ICollection<Address> Adress { get; set; }
    }
    public class AppUserviewModel : SharedModel
    {
        
        public string Name { get; set; }
       
        public string Email { get; set; }

        public virtual List<string> Role { get; set; }
    }
    public class LoginviewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public bool RememberMe { get; set; }
        [Required]
        public string Password { get; set; }


    }

    
}
