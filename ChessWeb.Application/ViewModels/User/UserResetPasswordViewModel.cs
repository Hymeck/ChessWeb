using System.ComponentModel.DataAnnotations;
using ChessWeb.Application.Constants.Labels;
using ChessWeb.Application.Stuff;

namespace ChessWeb.Application.ViewModels.User
{
    public class UserResetPasswordViewModel
    {
        public string Code { get; set; }
        
        [OwnRequired(EmailLabels.Email)]
        [Display(Name = EmailLabels.Email)]
        public string Email { get; set; }
        
        [OwnRequired(PasswordLabels.NewPassword)]
        [DataType(DataType.Password)]
        [Display(Name = PasswordLabels.NewPassword)]
        public string Password { get; set; }
        
        [OwnRequired(PasswordLabels.NewPasswordConfirm)]
        [Compare(nameof(Password), ErrorMessage = PasswordLabels.PasswordConfirmError)]
        [DataType(DataType.Password)]
        [Display(Name = PasswordLabels.NewPasswordConfirm)]
        public string ConfirmPassword { get; set; }
    }
}