using System.Linq;
using System.Threading.Tasks;
using ChessWeb.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ChessWeb.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;

namespace ChessWeb.Application.Controllers
{
    [Authorize(Roles = "администратор")]
    public class UsersController : Controller
    {
        UserManager<User> _userManager;
 
        public UsersController(UserManager<User> userManager) => 
            _userManager = userManager;

        public IActionResult Index() => 
            View(_userManager.Users.ToList());
 
        public IActionResult Create() => 
            View();
 
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { Email = model.Email, UserName = model.Name};
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "игрок");
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
 
        public async Task<IActionResult> Edit(string name)
        {
            if (name == null)
                return NotFound();
            var user = await _userManager.FindByNameAsync(name);
            if (user == null)
            {
                return NotFound();
            }
            var model = new UserEditViewModel {Id = user.Id, Name = user.UserName, Email = user.Email};
            return View(model);
        }
 
        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Name);
                if (user!=null)
                {
                    user.UserName = model.Name;
                    user.Email = model.Email;

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
        public async Task<ActionResult> Delete(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}