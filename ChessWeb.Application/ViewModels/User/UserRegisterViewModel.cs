using System.ComponentModel.DataAnnotations;
using ChessWeb.Application.Constants.Labels;

namespace ChessWeb.Application.ViewModels.User
{
    public class UserRegisterViewModel
    {
        [Required]
        [Display(Name = UserLabels.UserName)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [Display(Name = EmailLabels.Email)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = PasswordLabels.RegisterPassword)]
        public string Password { get; set; }
 
        [Required]
        [Compare(nameof(Password), ErrorMessage = PasswordLabels.PasswordConfirmError)]
        [DataType(DataType.Password)]
        [Display(Name = PasswordLabels.PasswordConfirm)]
        public string PasswordConfirm { get; set; }
    }
}