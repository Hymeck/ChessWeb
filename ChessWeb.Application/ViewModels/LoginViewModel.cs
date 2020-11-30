using System.ComponentModel.DataAnnotations;

namespace ChessWeb.Application.ViewModels
{
    public class LoginViewModel
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
         
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
         
        public string ReturnUrl { get; set; }
    }
}