using System.ComponentModel.DataAnnotations;

namespace TodoList.RequestHandler.Requests.Category
{
    public class UpdateCategoryRequest
    {
        [Required(ErrorMessage = "نام دسته بندی الزامی است.")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [RegularExpression("^#(?:[0-9a-fA-F]{3}){1,2}$", ErrorMessage = "لطفا یک کد رنگ معتبر وارد کنید")]
        public string? Color { get; set; }
    }
}
