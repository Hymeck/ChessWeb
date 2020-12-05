using System.Collections.Generic;
using System.Linq;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.UnitsOfWork;
using ChessWeb.Service.Interfaces;

namespace ChessWeb.Service.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GameService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Game> GetAll() =>
            _unitOfWork.Games.GetAll();

        public void CreateGame()
        {
            var whiteColor = _unitOfWork.Colors.Get(1);
            var gameStatus = _unitOfWork.GameStatuses.Get(1);
            var game = new Game();
            var gameSummary = new GameSummary(game, gameStatus, whiteColor);
            game.GameSummary = gameSummary;
            _unitOfWork.Games.Add(game);
            var blackColor = _unitOfWork.Colors.Get(2);
            var whiteSide = new Side {Color = whiteColor, Game = game};
            var blackSide = new Side {Color = blackColor, Game = game};
            _unitOfWork.Sides.Add(whiteSide);
            _unitOfWork.Sides.Add(blackSide);
            _unitOfWork.Complete();
        }

        public void Join(User user, long sideId)
        {
            var side = _unitOfWork.Sides.Get(sideId);
            side.User = user;
            _unitOfWork.Sides.Update(side);
            _unitOfWork.Complete();
        }

        public bool Any() =>
            _unitOfWork.Games.GetAll().Any();

        public IEnumerable<Game> GetUserGames(User user)
        {
            var games = _unitOfWork.Sides
                .GetAll()
                .Where(x => x.User?.Id == user.Id)
                .Select(x => x.Game);
            foreach (var game in games)
            {
                
            }
        }
    }
}