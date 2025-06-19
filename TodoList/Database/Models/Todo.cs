using Dapper.Contrib.Extensions;

namespace TodoList.Database.Models
{
    [Table("Todos")]
    public class Todo
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; } = string.Empty;
        public string? description { get; set; }
        public bool is_done { get; set; } = false;
        public int category_id { get; set; }
    }
}
