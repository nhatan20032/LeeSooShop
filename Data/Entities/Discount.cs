using ServiceStack.DataAnnotations;

namespace Data.Entities
{
    [Alias("discount")]
    public class Discount
    {
        public int id { get; set; }
        public required string title { get; set; }
        public float percent { get; set; } = 0;
        public required string image { get; set; }
        public string? description { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime started_time { get; set; }
        public DateTime? ended_time { get; set; } = null;
        public required string status { get; set; } = "active";
    }
}
