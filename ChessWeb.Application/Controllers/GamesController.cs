﻿using System;
using System.Linq;
using System.Threading.Tasks;
using ChessWeb.Application.ViewModels.Game;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.UnitsOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChessWeb.Application.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public GamesController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
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
        
        public async Task<IActionResult> Join(Game game, Side side)
        {
            var userName = HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            side.User = user;
            _unitOfWork.Sides.Update(side);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }
    }
}