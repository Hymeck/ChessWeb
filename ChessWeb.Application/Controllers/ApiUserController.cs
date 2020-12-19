using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessWeb.Application.DTO;
using ChessWeb.Domain.Entities;
using ChessWeb.Persistence.Contexts;
using ChessWeb.Persistence.Implementations;
using ChessWeb.Service.Interfaces;
using ChessWeb.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChessWeb.Application.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class ApiUsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IGameService _gameService;
        private readonly SideRepository _sideRepository;

        public ApiUsersController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IGameService gameService,
            SideRepository sideRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _gameService = gameService;
            _sideRepository = sideRepository;
        }

        [HttpGet("{username}/{password}")]
        public async Task<ActionResult<string>> GetUserId(string username, string password)
        {
            var result =
                await _signInManager.PasswordSignInAsync(username, password, isPersistent: false, false);
            if (!result.Succeeded)
                return NotFound();
            var user = await _userManager.FindByNameAsync(username);
            var userId = user.Id;
            await _signInManager.SignOutAsync();
            return userId;
        }
        
        [HttpGet("{gameId}/{userId}/{isWhite}")]
        public async Task<ActionResult<bool>> Join(long gameId, string userId, bool isWhite)
        {
            var game = await _gameService.GetAsync(gameId);
            if (game == null)
                return false;

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            if (isWhite && game.WhiteUserId != null)
                return false;

            else if (game.BlackUserId != null)
                return false;

            var sides = await _sideRepository.GetGameSides(game);
            //todo: shit place
            var side = sides.First(x => x.IsWhite == isWhite);

            await _gameService.JoinAsync(user, side);
            return true;
        }
    }
}