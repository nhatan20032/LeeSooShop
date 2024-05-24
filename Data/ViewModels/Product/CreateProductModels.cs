namespace Data.ViewModels.Product
{
    public class CreateProductModels
    {
        public int catalog_id { get; set; }
        public int age_id { get; set; }
        public int brand_id { get; set; }
        public int gender_id { get; set; }
        public int discount_id { get; set; }
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
        public string? status { get; set; } = "active";
    }
}
