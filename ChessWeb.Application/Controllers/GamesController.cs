using System;
using System.Linq;
using System.Threading.Tasks;
using ChessWeb.Application.Constants;
using ChessWeb.Application.ViewModels.Game;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChessWeb.Application.Controllers
{
    public class GamesController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IGameService _gameService;
        private readonly ISideRepository _sideRepository;
        private readonly IMoveRepository _moveRepository;
        
        public GamesController(
            UserManager<User> userManager, 
            IGameService gameService, 
            ISideRepository sideRepository,
            IMoveRepository moveRepository)
        {
            _userManager = userManager;
            _gameService = gameService;
            _sideRepository = sideRepository;
            _moveRepository = moveRepository;
        }

        public async Task<IActionResult> Index() =>
            View(await _gameService.GetAllAsync());

        [Authorize]
        public async Task<IActionResult> Create()
        {
            await _gameService.CreateGameAsync();
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [Authorize(Roles = Roles.AdminRole)]
        public async Task<IActionResult> Delete(long id)
        {
            var game = await _gameService.FindAsync(id);
            if (game == null)
                return NotFound();
            await _gameService.DeleteGameAsync(game);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GameSides(long id)
        {
            var game = await _gameService.GetAsync(id);
            if (game == null)
                return NotFound();
            var sides = await _sideRepository.GetGameSides(game);
            var moves = await _moveRepository.GetGameMoves(game);
            return View(GameViewModel.Instance(game, sides, moves));
        }

        public IActionResult Play(string userName)
        {
            throw new NotImplementedException(nameof(Play));
        }
        
        [Authorize]
        public async Task<IActionResult> Join(long sideId)
        {
            var userName = HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var side = await _sideRepository.FindAsync(sideId);
            await _gameService.JoinAsync(user, side);
            return RedirectToAction("Index");
        }
    }
}