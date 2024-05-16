using ServiceStack.DataAnnotations;

namespace Data.Entities
{
    [Alias("age")]
    public class Age
    {
        public int id { get; set; }
        public required string title { get; set; }
        public string? description { get; set; }
    }
}
