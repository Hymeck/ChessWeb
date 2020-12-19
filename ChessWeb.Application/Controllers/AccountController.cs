using System;
using System.Linq;
using System.Security.Claims;
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

        [HttpGet]
        public IActionResult EmailConfirmInfo() =>
            View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Неверные вводы");
                return View(model);
            }
                
            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("", "Некий иной юзверь зажал этот адрес электронной почты");
                return View(model);
            }
            
            if (await _userManager.FindByNameAsync(model.Name) != null)
            {
                ModelState.AddModelError("", "Некий иной юзверь зажал это погоняло");
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
                // return View("EmailConfirmInfo");
                return RedirectToAction("EmailConfirmInfo", "Account");
            }
                
            foreach (var error in result.Errors) 
                ModelState.AddModelError(string.Empty, error.Description);
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return RedirectToAction("Profile", "Account");
            
            return NotFound();
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            var viewModel = new UserLoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(viewModel);
        }
 
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model, string returnUrl = null)
        {
            // it need for not absent button of google auth when refresh the page
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Неверные вводы");
                return View(model);
            }
                
            var result =
                await _signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, false);
            if (result.Succeeded)
                return 
                    // !string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)
                    // ? Redirect(returnUrl)
                    // : RedirectToAction("Index", "Home");
                    RedirectToAction("Index", "Home");
                
            ModelState.AddModelError("", "Неправильное имя юзверя и (или?) пароль");
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
        public IActionResult ForgotPassword() => 
            View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(UserForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Неверные вводы");
                return View(model);
            }
                
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Небытие юзверя или нетвердый адрес электронной почты");
                return View(model);
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action(
                "ResetPassword", 
                "Account", 
                new { userId = user.Id, code = code }, 
                protocol: HttpContext.Request.Scheme);
            await _mailSender.SendMailAsync(user.Email, "Бросок пароля прогибом", $"Для броска пароля прогибом ступайте по мосту: <a href='{callbackUrl}'>мост</a>");
            // return View("ForgotPasswordConfirmation");
            return RedirectToAction("ForgotPasswordConfirmation", "Account");
        }
        
        [HttpGet]
        public IActionResult ForgotPasswordConfirmation() 
            => View();
        

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
        [HttpPost]
        public async Task<IActionResult> Confirm(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            await SendConfirmEmailAsync(user);
            
            ViewBag.Message = "Навестите почту и сделайте ее твердой";
            // return View("EmailConfirmInfo");
            return RedirectToAction("EmailConfirmInfo", "Account");
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
                ModelState.AddModelError("", "Неверные вводы");
                return View(model);
            }
            
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return View("Error");
            
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
                // return View("ResetPasswordConfirmation");
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            
            foreach (var error in result.Errors) 
                ModelState.AddModelError(string.Empty, error.Description);
            return View(model);
        }
        
        [HttpGet]
        public IActionResult ResetPasswordConfirmation() => 
            View();

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
                return RedirectToAction("Profile", "Account");

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(UserChangePasswordViewModel model)
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                ModelState.AddModelError("", "Пж, при сторонней авторизации пороль не нужен");
                return RedirectToAction("Profile", "Account");
            }
            
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Неверные вводы");
                return View();
            }
            
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
                return RedirectToAction("ChangePasswordConfirmation", "Account");
            ModelState.AddModelError("", "Опрокидень смены пароля");
            return View();
        }
        
        [HttpGet]
        public IActionResult ChangePasswordConfirmation() => 
            View();
        
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action(
            "ExternalLoginCallback", 
            "Account", 
            new {ReturnUrl = returnUrl});
            
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }
        
        public async Task<IActionResult> ExternalLoginCallback(string returlUrl = null, string remoteEror = null)
        {
            returlUrl ??= Url.Content("~/");

            var viewModel = new UserLoginViewModel
            {
                ReturnUrl = returlUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteEror != null)
            {
                ModelState.AddModelError("", $"Беды со сторонниками: {remoteEror}");
                return View("Login", viewModel);
            }

            var userInfo = await _signInManager.GetExternalLoginInfoAsync();
            if (userInfo == null)
            {
                ModelState.AddModelError("", "Беды с загрузкой сторонних сведений");
                return View("Login", viewModel);
            }

            var result = await _signInManager
                .ExternalLoginSignInAsync(
                    userInfo.LoginProvider,
                    userInfo.ProviderKey, 
                    isPersistent: false, 
                    bypassTwoFactor: true);

            if (result.Succeeded)
                return LocalRedirect(returlUrl);

            var email = userInfo.Principal.FindFirstValue(ClaimTypes.Email);

            if (email == null)
                return View("Error");

            var username = email.Split('@')[0];
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                user = new User
                {
                    UserName = username,
                    Email = email
                };

                var createResult = await _userManager.CreateAsync(user);
                if (createResult.Succeeded) 
                    await _userManager.AddToRoleAsync(user, Roles.PlayerRole);
            }

            await _userManager.AddLoginAsync(user, userInfo);
            await _signInManager.SignInAsync(user, isPersistent: false);
            // return LocalRedirect(returlUrl);
            return RedirectToAction("Index", "Home");
        }
    }
}