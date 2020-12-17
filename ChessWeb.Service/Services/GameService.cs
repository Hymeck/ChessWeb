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
        private readonly IGameStatusRepository _gameStatusRepository;

        public GameService(IGameRepository gameRepository, ISideRepository sideRepository, IGameStatusRepository gameStatusRepository)
        {
            _gameRepository = gameRepository;
            _sideRepository = sideRepository;
            _gameStatusRepository = gameStatusRepository;
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _gameRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Game>> GetUserGamesAsync(User user) => 
            await _gameRepository.GetUserGamesAsync(user);

        public async Task CreateGameAsync() => 
            await _gameRepository.CreateAsync();

        public async Task<Game> FindAsync(long id) => 
            await _gameRepository.FindAsync(id);

        public async Task<Game> GetAsync(long id) => 
            await _gameRepository.GetAsync(id);

        public async Task JoinAsync(User user, Side side)
        {
            side.User = user;
            _sideRepository.Update(side);
            var game = await _gameRepository.FindAsync(side.GameId);
            user.Games.Add(game);
            
            if (side.IsWhite)
                (game.WhiteUser, game.WhiteUsername) = (user, user.UserName);
            else
                (game.BlackUser, game.BlackUsername) = (user, user.UserName);

            var activePlayerCount = _sideRepository.GetActiveGamePlayerCount(game);
            // 1 due to db have not changed when  _sideRepository.Update(side);
            if (activePlayerCount == 1)
                game.Status = (await _gameStatusRepository.PlayStatus()).Status;
            
            _gameRepository.Update(game);
            await _sideRepository.SaveChangesAsync();
        }

        public async Task DeleteGameAsync(Game game)
        {
            _gameRepository.Delete(game);
            await _gameRepository.SaveChangesAsync();
        }
    }
}