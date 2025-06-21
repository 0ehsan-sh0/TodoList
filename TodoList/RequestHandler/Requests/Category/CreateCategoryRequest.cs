using System.ComponentModel.DataAnnotations;

namespace TodoList.RequestHandler.Requests.Category
{
    public class CreateCategoryRequest
    {
        [Required(ErrorMessage = "نام دسته بندی الزامی است.")]
        public string name { get; set; } = string.Empty;
        public string? description { get; set; }
        [RegularExpression("^#(?:[0-9a-fA-F]{3}){1,2}$", ErrorMessage = "لطفا یک کد رنگ معتبر وارد کنید")]
        public string? color { get; set; }
    }
}
