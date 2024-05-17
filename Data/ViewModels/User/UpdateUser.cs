namespace Data.ViewModels.User
{
    public class UpdateUser
    {
        public int id { get; set; }
        public required string last_name { get; set; }
        public required string family_name { get; set; }
        public required string phone { get; set; }
        public required string gender { get; set; }
        public required string password { get; set; }
        public required string status { get; set; } = "active";
    }
}
