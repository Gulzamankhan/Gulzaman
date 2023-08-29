using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class OrderDetail:SharedModel
    {
        [Required]
        public string OrderId { get; set; }

        // Navigation property for the order to which this detail belongs
        public Order Order { get; set; }

        [Required]
        public string ProductId { get; set; }

        // Navigation property for the product in this detail
        public Product Product { get; set; }

        public int QuantityDemanded { get; set; }
        public int? QuantitySent { get; set; }

    }
}
