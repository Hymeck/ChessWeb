using System;
using System.Linq;
using ChessWeb.Application.ViewModels.Game;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.UnitsOfWork;
using Microsoft.AspNetCore.Mvc;

namespace ChessWeb.Application.Controllers
{
    public class GamesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GamesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index() =>
            View(_unitOfWork.Games.GetAll());

        public IActionResult Create()
        {
            var game = new Game();
            _unitOfWork.Games.Add(game);
            var whiteColor = _unitOfWork.Colors.Get(1);
            var blackColor = _unitOfWork.Colors.Get(2);
            var whiteSide = new Side {Color = whiteColor, Game = game};
            var blackSide = new Side {Color =blackColor, Game = game};
            _unitOfWork.Sides.Add(whiteSide);
            _unitOfWork.Sides.Add(blackSide);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        public IActionResult Delete()
        {
            throw new NotImplementedException(nameof(Delete));
        }

        public IActionResult GamePlayers(long id)
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
    }
}