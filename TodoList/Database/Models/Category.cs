using Dapper.Contrib.Extensions;


namespace TodoList.Database.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Color { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;
        public DateTime? Deleted_at { get; set; }
        public string Username { get; set; } = string.Empty;
    }
}
