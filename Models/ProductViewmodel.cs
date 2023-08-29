using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class ProductViewmodel
    {

        public string Id { get; set; }
        public DateTime? EntryTime { get; set; }
        public string Name { get; set; }
       
        public string Description { get; set; }
        
        public Decimal Price { get; set; }
      
        public string Brand { get; set; }
        
        public string Category { get; set; }

        public string imageUrl { get; set; }
       
        
    }
}
