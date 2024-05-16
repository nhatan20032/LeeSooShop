using ServiceStack.DataAnnotations;

namespace Data.Entities
{
    [Alias("gender")]
    public class Gender
    {
        public int id { get; set; }
        public required string title { get; set; }
        public string? description { get; set; }
    }
}
