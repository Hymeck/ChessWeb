using System.Threading.Tasks;
using ChessWeb.Application.Constants;
using ChessWeb.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ChessWeb.Application.ViewModels.User;
using ChessWeb.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ChessWeb.Application.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IGameService _gameService;
        private readonly IMailSender _mailSender;
        private readonly ILogger<AccountController> _logger;
        public AccountController(
            UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            IGameService gameService,
            IMailSender mailSender,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _gameService = gameService;
            _mailSender = mailSender;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register() => 
            View();

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                if (await _userManager.FindByEmailAsync(model.Email) != null)
                {
                    ModelState.AddModelError("", "Некий иной юзверь зажал этот адрес электронной почты");
                    return View(model);
                }
                
                var user = new User { UserName = model.Name, Email = model.Email};
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.PlayerRole);
                    await _signInManager.SignInAsync(user, false);
                    await SendConfirmEmailAsync(user);
                    
                    ViewBag.Message = "Навестите почту и сделайте ее твердой";
                    
                    // return RedirectToAction("Index", "Home");
                    return View("EmailConfirmInfo");
                }
                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogError($"Specified user does not exist. Controller: {nameof(AccountController)}. Action: {nameof(ConfirmEmail)}");
                return NotFound();
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return RedirectToAction("Profile", "Account");
            else
            {
                _logger.LogError($"Email confirm fails. Controller: {nameof(AccountController)}. Action: {nameof(ConfirmEmail)}");
                return NotFound();
            }
        }
        
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new UserLoginViewModel { ReturnUrl = returnUrl });
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = 
                    await _signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        return Redirect(model.ReturnUrl);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("Вводы", "Неправильное имя юзверя и (или?) пароль-король");
            }
            return View(model);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(UserForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError("", "Небытие юзверя или нетвердый адрес электронной почты");
                    return View("ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action(
                    "ResetPassword", 
                    "Account", 
                    new { userId = user.Id, code = code }, 
                    protocol: HttpContext.Request.Scheme);
                await _mailSender.SendMailAsync(user.Email, "Бросок пароля прогибом", $"Для броска пароля прогибом ступайте по мосту: <a href='{callbackUrl}'>мост</a>");
                return View("ForgotPasswordConfirmation");
            }
            
            return View(model);
        }
        

        [Authorize(Roles = Roles.PlayerRole)]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var userGames = await _gameService.GetUserGamesAsync(user);
            return View(new UserProfileViewModel(user, userGames));
        }

        private async Task SendConfirmEmailAsync(User user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action(
                "ConfirmEmail",
                "Account",
                new { userId = user.Id, code = code },
                protocol: HttpContext.Request.Scheme);
                    
            await _mailSender.SendMailAsync(user.Email, "Отвердевание адреса электронной почты",
                $"Сделайте адрес электронной почты твердым, перейдя по мосту: <a href='{callbackUrl}'>мост</a>");
        }

        [Authorize]
        public async Task<IActionResult> ConfirmAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            await SendConfirmEmailAsync(user);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null) => 
            code == null ? View("Error") : View();
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(UserResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Вводы", "Что-то не то-с");
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return View("ResetPasswordConfirmation");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
        
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(UserChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                ModelState.AddModelError("Вводы", "Что-то не то-с");
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
                return View("ChangePasswordConfirmation");
            ModelState.AddModelError("Вводы", "Что-то не то-с");
            return View("ChangePassword");
        }
    }
}