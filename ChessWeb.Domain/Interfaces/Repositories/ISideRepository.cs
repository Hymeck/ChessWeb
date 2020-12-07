using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChessWeb.Domain.Entities;

namespace ChessWeb.Domain.Interfaces.Repositories
{
    public interface ISideRepository : IGenericRepository<Side>
    {
        Task<IEnumerable<Side>> GetGameSides(Game game);
        int GetActiveGamePlayerCount(Game game);
    }
}