using System.ComponentModel.DataAnnotations;

namespace ChessWeb.Application.ViewModels.User
{
    public class RegisterUserViewModel
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Электронная почта")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
 
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}