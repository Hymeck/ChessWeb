using System.ComponentModel.DataAnnotations;
using ChessWeb.Application.Constants.Labels;

namespace ChessWeb.Application.ViewModels.User
{
    public class UserLoginViewModel
    {
        [Required]
        [Display(Name = UserLabels.UserName)]
        public string Name { get; set; }
         
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = PasswordLabels.Password)]
        public string Password { get; set; }
         
        [Display(Name = UserLabels.RememberMe)]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}