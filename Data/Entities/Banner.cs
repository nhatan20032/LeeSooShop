using ServiceStack.DataAnnotations;

namespace Data.Entities
{
	[Alias("banner")]
	public class Banner
	{
		public int id { get; set; }
		public required string title { get; set; }
		public string? description { get; set; }
		public required string imageUrl { get; set; }
		public DateTime created_at { get; set; } = DateTime.UtcNow;
	}
}
