using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using ChessWeb.Application.Constants;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Service.Interfaces;

namespace ChessWeb.Application
{
    public class DataInitializer
    {
        public static async Task InitializeAsync(
            UserManager<User> userManager, 
            RoleManager<UserRole> roleManager, 
            IGameRepository gameRepository)
        {
            var adminEmail = "admin@gmail.com";
            var adminPassword = "adminpass";
            var adminNickname = "admin";

            var adminRole = Roles.AdminRole;
            var playerRole = Roles.PlayerRole;
            
            if (await roleManager.FindByNameAsync(adminRole) == null)
            {
                await roleManager.CreateAsync(new UserRole(adminRole));
            }
            if (await roleManager.FindByNameAsync(playerRole) == null)
            {
                await roleManager.CreateAsync(new UserRole(playerRole));
            }

            // admin
            await AddUser(userManager, adminNickname, adminEmail, adminPassword, adminRole, playerRole);
            // hymeck
            await AddUser(userManager, "Hymeck", "noonimf@gmail.com", "hymeckpass", playerRole);
            // racoty
            await AddUser(userManager, "Racoty", "mr.yatson@gmail.com", "racotypass", playerRole);
            
            await AddGame(gameRepository);
        }

        private static async Task AddUser(UserManager<User> userManager, string nickname, string email, string password, params string[] roles)
        {
            if (await userManager.FindByNameAsync(nickname) == null)
            {
                var admin = new User { Email = email, UserName = nickname };
                var result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    foreach (var role in roles)
                        await userManager.AddToRoleAsync(admin, role);
                }
            }
        }

        private static async Task AddGame(IGameRepository gameRepository)
        {
            if (!gameRepository.Any())
                await gameRepository.CreateGameAsync();
        }
    }
}