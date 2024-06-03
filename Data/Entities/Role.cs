using ServiceStack.DataAnnotations;

namespace Data.Entities
{
    [Alias("role")]
    public class Role
    {
        public int id { get; set; }
        public required string title { get; set; }
        public string? description { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public required string status { get; set; } = "active";
    }
}
