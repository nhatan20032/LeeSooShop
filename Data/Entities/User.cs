using ServiceStack.DataAnnotations;

namespace Data.Entities
{
    [Alias("user")]
    public class User
    {
        public int id { get; set; }
        public required string last_name { get; set; }
        public required string family_name { get; set; }
        public required string phone { get; set; }
        public required string gender { get; set; }
        public required string email { get; set; }
        public required string password { get; set; }
        public string? token { get; set; }
        public TimeOnly? token_expired { get; set; } = null;
        public required string status { get; set; } = "active";
    }
}
