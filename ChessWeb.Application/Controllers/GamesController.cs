using System;
using System.Linq;
using System.Threading.Tasks;
using ChessWeb.Application.ViewModels.Game;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.UnitsOfWork;
using ChessWeb.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChessWeb.Application.Controllers
{
    public class GamesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IGameService _gameService;
        public GamesController(IUnitOfWork unitOfWork, UserManager<User> userManager, IGameService gameService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _gameService = gameService;
        }

        public IActionResult Index() =>
            View(_gameService.GetAll());

        [Authorize]
        public IActionResult Create()
        {
            _gameService.CreateGame();
            return RedirectToAction("Index");
        }

        public IActionResult Delete()
        {
            throw new NotImplementedException(nameof(Delete));
        }

        public IActionResult GameSides(long id)
        {
            var game = _unitOfWork.Games.Get(id);
            if (game == null)
                return NotFound();
            var sides = _unitOfWork.Sides.GetAll().Where(x => x.Game == game);
            return View(new GameViewModel(game, sides));
        }

        public IActionResult Play(string userName)
        {
            throw new NotImplementedException(nameof(Play));
        }
        
        [Authorize]
        public IActionResult Join(long sideId)
        {
            var userName = HttpContext.User.Identity.Name;
            var user = _userManager.FindByNameAsync(userName).Result;
            _gameService.Join(user, sideId);
            return RedirectToAction("Index");
        }
    }
}