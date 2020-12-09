using System.ComponentModel.DataAnnotations;

namespace ChessWeb.Application.ViewModels.User
{
    public class UserChangePasswordViewModel
    {
        [Required]
        [Display(Name = "Старый король")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        
        [Required]
        [Display(Name = "Новый король")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        
        [Required]
        [Display(Name = "Еще раз новый король")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}