using ServiceStack.DataAnnotations;

namespace Data.Entities
{
    [Alias("product_discount")]
    public class Product_Discount
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public int discount_id { get; set; }
    }
}
