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
        private readonly IGameSummaryRepository _gameSummaryRepository;

        public GameService(IGameRepository gameRepository, ISideRepository sideRepository, IGameStatusRepository gameStatusRepository, IGameSummaryRepository gameSummaryRepository)
        {
            _gameRepository = gameRepository;
            _sideRepository = sideRepository;
            _gameStatusRepository = gameStatusRepository;
            _gameSummaryRepository = gameSummaryRepository;
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _gameRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Game>> GetUserGamesAsync(User user) => 
            await _gameRepository.GetUserGamesAsync(user);

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

            var activePlayerCount = _sideRepository.GetActiveGamePlayerCount(game);
            // 1 due to db have not changed when  _sideRepository.UpdateAsync(side);
            if (activePlayerCount == 1)
            {
                var gameSummary = await _gameSummaryRepository.FindAsync(game.GameSummaryId);
                gameSummary.Status = (await _gameStatusRepository.Play()).Status;
                await _gameSummaryRepository.UpdateAsync(gameSummary);
            }
                
            
            await _gameRepository.UpdateAsync(game);
            await _sideRepository.SaveChangesAsync();
        }

        public async Task DeleteGameAsync(Game game)
        {
            await _gameRepository.DeleteAsync(game);
            await _gameRepository.SaveChangesAsync();
        }

        public bool Any()
        {
            throw new System.NotImplementedException();
        }
    }
}