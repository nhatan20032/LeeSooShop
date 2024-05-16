using ServiceStack.DataAnnotations;

namespace Data.Entities
{
    [Alias("catalog")]
    public class Catalog
    {
        public int id { get; set; }
        public int parent_id { get; set; }
        public required string title { get; set; }
        public string? description { get; set; }
    }
}
