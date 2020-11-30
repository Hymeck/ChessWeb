using System.Threading.Tasks;
using ChessWeb.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ChessWeb.Application.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace ChessWeb.Application.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Player> _userManager;
        private readonly SignInManager<Player> _signInManager;
 
        public AccountController(UserManager<Player> userManager, SignInManager<Player> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new Player { Nickname = model.Nickname, Email = model.Email, UserName = model.Email};
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
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
        
        // [HttpGet]
        // [AllowAnonymous]
        // public async Task<IActionResult> ConfirmEmail(string userId, string code)
        // {
        //     if (userId == null || code == null)
        //     {
        //         return View("Error");
        //     }
        //     var user = await _userManager.FindByIdAsync(userId);
        //     if (user == null)
        //     {
        //         return View("Error");
        //     }
        //     var result = await _userManager.ConfirmEmailAsync(user, code);
        //     if(result.Succeeded)
        //         return RedirectToAction("Index", "Home");
        //     else
        //         return View("Error");
        // }
        
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = 
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}