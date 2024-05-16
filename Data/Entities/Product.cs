using ServiceStack.DataAnnotations;

namespace Data.Entities
{
    [Alias("product")]
    public class Product
    {
        public int id { get; set; }
        public required string code { get; set; }
        public required string title { get; set; }
        public string? description { get; set; }
        public float price { get; set; } = 0;
        public int amount { get; set; } = 0;
        public required string image_1 { get; set; }
        public string? image_2 { get; set; }
        public string? image_3 { get; set; }
        public string? image_4 { get; set; }
        public required string source { get; set; }
        public required string gender { get; set; }
        public required string age { get; set; }
        public required DateTime created_at { get; set; } = DateTime.Now;
        public DateTime? modified_at { get; set; } = null;
        public required string status { get; set; } = "active";
    }
}
