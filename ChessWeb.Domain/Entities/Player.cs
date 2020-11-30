using Microsoft.AspNetCore.Identity;

namespace ChessWeb.Domain.Entities
{
    public class Player : IdentityUser
    {
        public string Nickname { get; set; }
    }
}