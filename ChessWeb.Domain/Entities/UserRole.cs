using Microsoft.AspNetCore.Identity;

namespace ChessWeb.Domain.Entities
{
    public class UserRole : IdentityRole<long>
    {
        public UserRole() {}

        public UserRole(string roleName) =>
            Name = roleName;
    }
}