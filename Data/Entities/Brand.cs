using ServiceStack.DataAnnotations;

namespace Data.Entities
{
    [Alias("brand")]
    public class Brand
    {
        public int id { get; set; }
        public required string code { get; set; }
        public required string title { get; set; }
        public string? description { get; set; }
        public required string source { get; set; }
        public DateTime? created_at { get; set; } = DateTime.Now;
        public DateTime? modified_at { get; set; } = null;
    }
}
