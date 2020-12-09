using System.ComponentModel.DataAnnotations;

namespace ChessWeb.Application.ViewModels.User
{
    public class UserForgotPasswordViewModel
    {
        [Required]
        [Display(Name = "Забытый волхвами адрес")]
        public string Email { get; set; }
    }
}