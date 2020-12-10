using System.ComponentModel.DataAnnotations;
using ChessWeb.Application.Constants.Labels;
using ChessWeb.Application.Stuff;

namespace ChessWeb.Application.ViewModels.User
{
    public class UserChangePasswordViewModel
    {
        [OwnRequired(PasswordLabels.OldPassword)]
        [Display(Name = PasswordLabels.OldPassword)]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        
        [OwnRequired(PasswordLabels.NewPassword)]
        [Display(Name = PasswordLabels.NewPassword)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        
        [OwnRequired(PasswordLabels.NewPasswordConfirm)]
        [Display(Name = PasswordLabels.NewPasswordConfirm)]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}