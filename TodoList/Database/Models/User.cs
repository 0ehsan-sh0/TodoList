using Dapper.Contrib.Extensions;

namespace TodoList.Database.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        // this field just accept two inputs : "user" and "admin"
        public string Role { get; set; } = "user";
    }
}
