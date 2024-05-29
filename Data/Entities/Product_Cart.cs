using ServiceStack.DataAnnotations;

namespace Data.Entities
{
    [Alias("product_cart")]
    public class Product_Cart
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
        public int cart_id { get; set; }
    }
}
