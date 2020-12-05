using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Service.Interfaces;

namespace ChessWeb.Service.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ISideRepository _sideRepository;
        // public void Join(User user, long sideId)
        // {
        //     var side = _unitOfWork.Sides.GetAsync(sideId);
        //     side.User = user;
        //     _unitOfWork.Sides.Update(side);
        //     _unitOfWork.Complete();
        // }
        //
        // public bool Any() =>
        //     _unitOfWork.Games.GetAll().Any();
        //
        // public IEnumerable<Game> GetUserGames(User user)
        // {
        //     var games = _unitOfWork.Sides
        //         .GetAll()
        //         .Where(x => x.User?.Id == user.Id)
        //         .Select(x => x.Game);
        //     foreach (var game in games)
        //     {
        //         
        //     }
        // }
        public GameService(IGameRepository gameRepository, ISideRepository sideRepository)
        {
            _gameRepository = gameRepository;
            _sideRepository = sideRepository;
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _gameRepository.GetAllAsync();
        }

        public async Task CreateGameAsync()
        {
            await _gameRepository.CreateGameAsync();
        }

        public async Task<Game> FindAsync(long id)
        {
            return await _gameRepository.FindAsync(id);
        }

        public async Task<Game> GetAsync(long id)
        {
            return await _gameRepository.GetAsync(id);
        }

        public async Task JoinAsync(User user, Side side)
        {
            side.User = user;
            await _sideRepository.UpdateAsync(side);
            var game = await _gameRepository.FindAsync(side.GameId);
            user.Games.Add(game);
            if (side.IsWhite)
                game.WhiteUser = user;
            else
                game.BlackUser = user;

            await _sideRepository.SaveChangesAsync();
        }

        public bool Any()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Game> GetUserGamesAsync(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}