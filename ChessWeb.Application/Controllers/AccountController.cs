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
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new User { UserName = model.Name, Email = model.Email};
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.PlayerRole);
                    await _signInManager.SignInAsync(user, false);
                    await SendConfirmEmailAsync(user);
                    return RedirectToAction("Index", "Home");
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

                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
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
                    
            await _mailSender.SendMailAsync(user.Email, "Подтверждение учетной записи",
                $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

        }

        [Authorize]
        public async Task<IActionResult> Confirm(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            await SendConfirmEmailAsync(user);
            return RedirectToAction("Index", "Home");
        }
    }
}