using System.ComponentModel.DataAnnotations;

namespace ChessWeb.Application.ViewModels.User
{
    public class UserResetPasswordViewModel
    {
        public string Code { get; set; }
        
        [Required]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string Password { get; set; }
        
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Пересечение указанных паролей дает пустое множество")]
        [DataType(DataType.Password)]
        [Display(Name = "Еще раз новый пароль")]
        public string ConfirmPassword { get; set; }
    }
}