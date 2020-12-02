using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using ChessWeb.Domain.Entities;

namespace ChessWeb.Application
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<UserRole> roleManager)
        {
            var adminEmail = "admin@gmail.com";
            var password = "adminpass";
            var nickname = "admin";

            var role1 = "admin";
            var role2 = "player";
            
            if (await roleManager.FindByNameAsync(role1) == null)
            {
                await roleManager.CreateAsync(new UserRole(role1));
            }
            if (await roleManager.FindByNameAsync(role2) == null)
            {
                await roleManager.CreateAsync(new UserRole(role2));
            }
            if (await userManager.FindByNameAsync(nickname) == null)
            {
                var admin = new User { Email = adminEmail, UserName = nickname };
                var result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, role1);
                }
            }
        }
    }
}