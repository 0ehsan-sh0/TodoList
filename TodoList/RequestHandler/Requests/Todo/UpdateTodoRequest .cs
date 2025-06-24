using System.ComponentModel.DataAnnotations;

namespace TodoList.RequestHandler.Requests.Todo
{
    public class UpdateTodoRequest
    {
        [Required(ErrorMessage = "عنوان تسک الزامی است")]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required(ErrorMessage = "دسته بندی تسک الزامی است")]
        public int Category_id { get; set; }
        [Required(ErrorMessage = "وضعیت تسک الزامی است")]
        public bool Is_done { get; set; } = false;
    }
}
