using Microsoft.AspNetCore.Identity;

namespace ChessWeb.Domain.Entities
{
    public class UserRole : IdentityRole
    {
        public UserRole() {}

        public UserRole(string roleName) =>
            Name = roleName;
    }
}