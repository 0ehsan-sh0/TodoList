﻿namespace TodoList.RequestHandler.Responces.Category
{
    public class RCategory
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string? description { get; set; }
        public string? color { get; set; }
        public DateTime created_at { get; set; }
    }
}
