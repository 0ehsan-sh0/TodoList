using System.ComponentModel.DataAnnotations;

namespace TodoList.RequestHandler.Requests.Category
{
    public class CreateCategoryRequest
    {
        [Required(ErrorMessage = "نام دسته بندی الزامی است.")]
        [MaxLength(255, ErrorMessage = "نام کاربری نباید بیش از 255 کاراکتر باشد")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [RegularExpression("^#(?:[0-9a-fA-F]{3}){1,2}$", ErrorMessage = "لطفا یک کد رنگ معتبر وارد کنید")]
        public string? Color { get; set; }
    }
}
