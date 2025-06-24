namespace TodoList.Database.Models
{
    public class TodoWithCName
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool Is_done { get; set; } = false;
        public string Category_name { get; set; } = string.Empty;
        public int Category_id { get; set; }
    }
}
