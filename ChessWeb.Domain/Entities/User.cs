using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ChessWeb.Domain.Entities
{
    public class User : IdentityUser
    {
        public List<Game> Games { get; set; } = new();
        public override string ToString() =>
            UserName;
    }
}