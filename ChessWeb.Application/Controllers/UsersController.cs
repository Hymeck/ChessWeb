using System.Linq;
using System.Threading.Tasks;
using ChessWeb.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ChessWeb.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ChessWeb.Application.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        UserManager<Player> _userManager;
 
        public UsersController(UserManager<Player> userManager) => 
            _userManager = userManager;

        public IActionResult Index() => 
            View(_userManager.Users.ToList());
 
        public IActionResult Create() => 
            View();
 
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Player { Nickname = model.Nickname, Email = model.Email, UserName = model.Email};
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
 
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new EditUserViewModel {Id = user.Id, Nickname = user.Nickname, Email = user.Email,};
            return View(model);
        }
 
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if(user!=null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                     
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }
 
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}