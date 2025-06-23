using Dapper.Contrib.Extensions;

namespace TodoList.Database.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        // this field just accept two inputs : "user" and "admin"
        public string role { get; set; } = "user";
    }
}
