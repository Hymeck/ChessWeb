using System.ComponentModel.DataAnnotations;
using ChessWeb.Application.Constants.Labels;

namespace ChessWeb.Application.ViewModels.User
{
    public class UserResetPasswordViewModel
    {
        public string Code { get; set; }
        
        [Required]
        [Display(Name = EmailLabels.Email)]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = PasswordLabels.NewPassword)]
        public string Password { get; set; }
        
        [Required]
        [Compare(nameof(Password), ErrorMessage = PasswordLabels.PasswordConfirmError)]
        [DataType(DataType.Password)]
        [Display(Name = PasswordLabels.NewPasswordConfirm)]
        public string ConfirmPassword { get; set; }
    }
}