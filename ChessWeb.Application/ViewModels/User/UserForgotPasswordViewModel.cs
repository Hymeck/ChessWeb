using System.ComponentModel.DataAnnotations;
using ChessWeb.Application.Constants.Labels;
using ChessWeb.Application.Stuff;

namespace ChessWeb.Application.ViewModels.User
{
    public class UserForgotPasswordViewModel
    {
        [OwnRequired(EmailLabels.ForgotPasswordEmail)]
        [Display(Name = EmailLabels.ForgotPasswordEmail)]
        public string Email { get; set; }
    }
}