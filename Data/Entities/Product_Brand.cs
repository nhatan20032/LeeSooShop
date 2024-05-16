using ServiceStack.DataAnnotations;

namespace Data.Entities
{
    [Alias("product_brand")]
    public class Product_Brand
    {
        public int id { get; set; }
        public int brand_id { get; set; }
        public int product_id { get; set; }
    }
}
