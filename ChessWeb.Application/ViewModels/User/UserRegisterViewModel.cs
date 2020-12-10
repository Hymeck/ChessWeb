using System.ComponentModel.DataAnnotations;
using ChessWeb.Application.Constants.Labels;
using ChessWeb.Application.Stuff;

namespace ChessWeb.Application.ViewModels.User
{
    public class UserRegisterViewModel
    {
        [OwnRequired(UserLabels.UserName)]
        [Display(Name = UserLabels.UserName)]
        [MaxLength(30)]
        public string Name { get; set; }

        [OwnRequired(EmailLabels.Email)]
        [Display(Name = EmailLabels.Email)]
        public string Email { get; set; }

        [OwnRequired(PasswordLabels.RegisterPassword)]
        [DataType(DataType.Password)]
        [Display(Name = PasswordLabels.RegisterPassword)]
        public string Password { get; set; }
 
        [OwnRequired(PasswordLabels.PasswordConfirm)]
        [Compare(nameof(Password), ErrorMessage = PasswordLabels.PasswordConfirmError)]
        [DataType(DataType.Password)]
        [Display(Name = PasswordLabels.PasswordConfirm)]
        public string PasswordConfirm { get; set; }
    }
}