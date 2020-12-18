using System.Linq;
using System.Threading.Tasks;
using ChessWeb.Application.Constants;
using ChessWeb.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ChessWeb.Application.Controllers
{
    [Authorize(Roles = Roles.AdminRole)]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
 
        public UsersController(UserManager<User> userManager) => 
            _userManager = userManager;

        public IActionResult Index() => 
            View(_userManager.Users.ToList());

        [HttpPost]
        public async Task<IActionResult> Delete(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return NotFound();
            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                ModelState.AddModelError("", "Беды с удалением");

            return RedirectToAction("Index", "Users");
        }
    }
}