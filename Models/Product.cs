


using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class Product:SharedModel
    {
        public Product()
        {
            Images = new();
        }
        [Required(ErrorMessage ="Name Is Required")]
        [MaxLength(30)]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required(ErrorMessage ="Description Is Required")]
        [DataType (DataType.MultilineText)]
        public string Description { get; set; }
        [Required(ErrorMessage ="Price Is Required")]
        [DataType (DataType.Currency)]
        public Decimal Price { get; set; }
        [Required(ErrorMessage ="Brand Is Required")]
        [DataType (DataType.Text)]
        [MaxLength (30)]
         public string Brand { get; set; }
        [NotMapped]
         public List<IFormFile> UploudImages { get; set; }
        public List<Image> Images { get; set; }
        public ProductStatus Status { get; set; }
     
        [Required]
        [ForeignKey("CategoryId")]
        public string CategoryId { get; set; }
        
        public virtual Category Category { get; set; }
        public virtual ICollection<WishList> WishList { get; set; }
        
    }
    public class Image:SharedModel
    {
      
        public string Path { get; set; }
        public int Rank { get; set; }
        [ForeignKey("ProductId")]
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
    public enum ProductStatus
    {
        Ative = 0, Inactive = 10, UpComing = 20
    }
}
