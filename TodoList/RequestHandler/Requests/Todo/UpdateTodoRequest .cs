using System.ComponentModel.DataAnnotations;

namespace TodoList.RequestHandler.Requests.Todo
{
    public class UpdateTodoRequest
    {
        [Required(ErrorMessage = "عنوان تسک الزامی است")]
        public string title { get; set; } = string.Empty;
        public string? description { get; set; }
        [Required(ErrorMessage = "دسته بندی تسک الزامی است")]
        public int category_id { get; set; }
    }
}
