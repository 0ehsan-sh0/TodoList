namespace TodoList.RequestHandler.Responces.Todo
{
    public class RTodo
    {
        public int id { get; set; }
        public string title { get; set; } = string.Empty;
        public string? description { get; set; }
        public bool is_done { get; set; } = false;
        public int category_id { get; set; }
        public string? category_name { get; set; }
    }
}
