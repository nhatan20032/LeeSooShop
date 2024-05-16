using ServiceStack.DataAnnotations;

namespace Data.Entities
{
    [Alias("product_catalog")]
    public class Product_Catalog
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public int catalog_id { get; set; }
    }
}
