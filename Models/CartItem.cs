namespace E_Commerce.Models
{
    public class CartItem:SharedModel
    {
       
        public string ShoppingCartId { get; set; }

        // Navigation property for the shopping cart to which this item belongs
        public ShopingCart ShoppingCart { get; set; }

        
        public string ProductId { get; set; }

        // Navigation property for the product in this cart item
        public Product Product { get; set; }

        public int Quantity { get; set; }

        // Additional relevant details for the cart item
    }
}

