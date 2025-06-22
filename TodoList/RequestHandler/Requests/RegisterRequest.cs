using System.ComponentModel.DataAnnotations;

namespace TodoList.RequestHandler.Requests
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "نام کاربری الزامی است")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "رمز عبور الزامی است")]
        [MinLength(8, ErrorMessage = "رمز عبور حداقل باید هشت کاراکتر باشد")]
        public string Password { get; set; } = string.Empty;
        [Required]
        [MinLength(8, ErrorMessage = "تکرار رمز عبور حداقل باید هشت کاراکتر باشد")]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن مغایرت دارند")]
        public string PasswordConfirmation { get; set; } = string.Empty;
    }
}
