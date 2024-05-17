using ServiceStack.DataAnnotations;

namespace Data.Entities
{
    [Alias("user_role")]
    public class User_Role
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int role_id { get; set; }
    }
}
