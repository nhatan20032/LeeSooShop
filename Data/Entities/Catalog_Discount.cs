using ServiceStack.DataAnnotations;

namespace Data.Entities
{
    [Alias("catalog_discount")]
    public class Catalog_Discount
    {
        public int id { get; set; }
        public int catalog_id { get; set; }
        public int discount_id { get; set; }
    }
}
