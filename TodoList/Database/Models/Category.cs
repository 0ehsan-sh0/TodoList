using Dapper.Contrib.Extensions;


namespace TodoList.Database.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string? description { get; set; }
        public string? color { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime? deleted_at { get; set; }
    }
}
