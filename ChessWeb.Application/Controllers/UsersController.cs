using System.Linq;
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
        private UserManager<User> _userManager;
 
        public UsersController(UserManager<User> userManager) => 
            _userManager = userManager;

        public IActionResult Index() => 
            View(_userManager.Users.ToList());
    }
}