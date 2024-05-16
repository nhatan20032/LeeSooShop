using ServiceStack.DataAnnotations;

namespace Data.Entities
{
    [Alias("cart")]
    public class Cart
    {
        public int id { get; set; }
        public int amount { get; set; } = 0;
        public float total_price { get; set; } = 0;
    }
}
