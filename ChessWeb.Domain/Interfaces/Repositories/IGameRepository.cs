using System.Collections.Generic;
using ChessWeb.Domain.Entities;

namespace ChessWeb.Domain.Interfaces.Repositories
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        IEnumerable<Game> GetUserGames(User user);
    }
}