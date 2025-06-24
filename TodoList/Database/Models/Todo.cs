using Dapper.Contrib.Extensions;

namespace TodoList.Database.Models
{
    [Table("Todos")]
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool Is_done { get; set; } = false;
        public int Category_id { get; set; }
    }
}
