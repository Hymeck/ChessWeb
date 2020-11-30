using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using ChessWeb.Domain.Entities;

namespace ChessWeb.Application
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<Player> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminEmail = "admin@gmail.com";
            var password = "adminpass";
            var nickname = "adminNickname";

            var role1 = "admin";
            var role2 = "player";
            
            if (await roleManager.FindByNameAsync(role1) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role1));
            }
            if (await roleManager.FindByNameAsync(role2) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role2));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                var admin = new Player { Nickname = nickname, Email = adminEmail, UserName = adminEmail };
                var result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, role1);
                }
            }
        }
    }
}