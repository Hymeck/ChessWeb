using System.ComponentModel.DataAnnotations;
using ChessWeb.Application.Constants.Labels;

namespace ChessWeb.Application.ViewModels.User
{
    public class UserForgotPasswordViewModel
    {
        [Required]
        [Display(Name = EmailLabels.ForgotPasswordEmail)]
        public string Email { get; set; }
    }
}