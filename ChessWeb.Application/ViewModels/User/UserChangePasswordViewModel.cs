using System.ComponentModel.DataAnnotations;
using ChessWeb.Application.Constants.Labels;

namespace ChessWeb.Application.ViewModels.User
{
    public class UserChangePasswordViewModel
    {
        [Required]
        [Display(Name = PasswordLabels.OldPassword)]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        
        [Required]
        [Display(Name = PasswordLabels.NewPassword)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        
        [Required]
        [Display(Name = PasswordLabels.NewPasswordConfirm)]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}