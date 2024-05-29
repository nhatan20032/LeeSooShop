using ServiceStack.DataAnnotations;

namespace Data.Entities
{
    [Alias("cart")]
    public class Cart
    {
        public int id { get; set; }
        public int total_product { get; set; } = 0;
        public float total_price { get; set; } = 0;
    }
    [Alias("v_cartproduct")]
    public class v_Cart
    {
        public int id { get; set; }
        public int cart_id { get; set; }
        public string? code { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public string? image_1{ get; set; }
        public string? image_2{ get; set; }
        public string? image_3{ get; set; }
        public string? image_4{ get; set; }
        public string? source{ get; set; }
        public string? gender{ get; set; }
        public string? age{ get; set; }
        public int quantity{ get; set; }
    }
}
