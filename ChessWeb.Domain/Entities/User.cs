using Microsoft.AspNetCore.Identity;

namespace ChessWeb.Domain.Entities
{
    public class User : IdentityUser<long>
    {
        public override string ToString() =>
            UserName;
    }
}