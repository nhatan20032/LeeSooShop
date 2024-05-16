using ServiceStack.DataAnnotations;

namespace Data.Entities
{
    [Alias("product_gender")]
    public class Product_Gender
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public int gender_id { get; set; }
    }
}
