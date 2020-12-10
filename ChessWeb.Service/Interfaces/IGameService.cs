using System.Collections.Generic;
using System.Threading.Tasks;
using ChessWeb.Domain.Entities;

namespace ChessWeb.Service.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetAllAsync();
        Task<IEnumerable<Game>> GetUserGamesAsync(User user);
        Task CreateGameAsync();
        Task<Game> FindAsync(long id);
        Task<Game> GetAsync(long id);
        Task JoinAsync(User user, Side side);
        Task DeleteGameAsync(Game game);
    }
}