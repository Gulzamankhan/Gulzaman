

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class Category:SharedModel
    {
        [Required(ErrorMessage ="Name Is Requierd")]
        [DataType(DataType.Text)]
        [Remote("Index","Remote","Id",ErrorMessage ="Name already exsist")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Description Is Requierd")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
       
        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }
        [NotMapped]
        [DataType(DataType.ImageUrl)]
        public IFormFile ImgeLogo { get; set; }
        public bool Status { get; set; }
        public  CategoryType Type { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
    public enum CategoryType
    { 
         Category=0,
         Brand=10,
         Blogs=20,
    
    }

}
