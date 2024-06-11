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

	[Alias("v_catalog")]
	public class v_Catalog
	{
		public int id { get; set; }
		public int parent_id { get; set; }
		public string? parent_title { get; set; }
		public string? catalog_title { get; set; }
		public string? description { get; set; }
	}
}
