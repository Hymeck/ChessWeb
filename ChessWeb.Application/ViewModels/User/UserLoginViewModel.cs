using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ChessWeb.Application.Constants.Labels;
using ChessWeb.Application.Stuff;
using Microsoft.AspNetCore.Authentication;

namespace ChessWeb.Application.ViewModels.User
{
    public class UserLoginViewModel
    {
        [OwnRequired(UserLabels.UserName)]
        [Display(Name = UserLabels.UserName)]
        public string Name { get; set; }
         
        [OwnRequired(PasswordLabels.Password)]
        [DataType(DataType.Password)]
        [Display(Name = PasswordLabels.Password)]
        public string Password { get; set; }
         
        [Display(Name = UserLabels.RememberMe)]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
        
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}