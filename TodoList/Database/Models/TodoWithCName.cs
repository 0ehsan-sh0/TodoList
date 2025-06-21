namespace TodoList.Database.Models
{
    public class TodoWithCName
    {
        public int id { get; set; }
        public string title { get; set; } = string.Empty;
        public string? description { get; set; }
        public bool is_done { get; set; } = false;
        public string category_name { get; set; } = "";
        public int category_id { get; set; }
    }
}
