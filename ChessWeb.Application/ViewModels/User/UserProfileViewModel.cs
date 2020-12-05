using System.Collections.Generic;

namespace ChessWeb.Application.ViewModels.User
{
    public class UserProfileViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public IEnumerable<Domain.Entities.Game> UserGames { get; set; }

        public UserProfileViewModel(Domain.Entities.User user, IEnumerable<Domain.Entities.Game> games)
        {
            Name = user.UserName;
            Email = user.Email;
            EmailConfirmed = user.EmailConfirmed;
            UserGames = games;
        }
    }
}