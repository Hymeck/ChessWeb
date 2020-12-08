using System.Collections.Generic;
using System.Threading.Tasks;
using ChessWeb.Domain.Entities;

namespace ChessWeb.Domain.Interfaces.Repositories
{
    public interface IMoveRepository : IGenericRepository<Move>
    {
        Task<IEnumerable<Move>> GetAllAsync();
        Task<IEnumerable<Move>> GetGameMoves(Game game);
    }
}