using System.Collections.Generic;
using System.Threading.Tasks;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Persistence.Contexts;

namespace ChessWeb.Persistence.Implementations
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        public GameRepository(ApplicationDbContext dbContext) : base(dbContext) {}

        public Task<IEnumerable<Game>> GetUserGames(User user)
        {
            throw new System.NotImplementedException(nameof(GetUserGames));
        }

        public Task CreateGame()
        {
            throw new System.NotImplementedException(nameof(CreateGame));
        }

        public Task Join(User user, Side side)
        {
            throw new System.NotImplementedException(nameof(Join));
        }
    }
}