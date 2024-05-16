using ServiceStack.DataAnnotations;

namespace Data.Entities
{
    [Alias("product_age")]
    public class Product_Age
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public int age_id { get; set; }
    }
}
