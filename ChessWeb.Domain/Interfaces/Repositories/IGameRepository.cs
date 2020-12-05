using System.Collections.Generic;
using System.Threading.Tasks;
using ChessWeb.Domain.Entities;

namespace ChessWeb.Domain.Interfaces.Repositories
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        Task<IEnumerable<Game>> GetUserGames(User user);
        Task CreateGame();
        Task Join(User user, Side side);
    }
}