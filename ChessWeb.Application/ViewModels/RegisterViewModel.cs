using System.ComponentModel.DataAnnotations;

namespace ChessWeb.Application.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        public string Nickname { get; set; }

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