namespace Data.Models
{
    public class DataTableResult
    {
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public required object data { get; set; }
    }
}
