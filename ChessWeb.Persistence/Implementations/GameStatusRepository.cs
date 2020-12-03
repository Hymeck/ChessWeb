using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Persistence.Contexts;

namespace ChessWeb.Persistence.Implementations
{
    public class GameStatusRepository : GenericRepository<GameStatus>, IGameStatusRepository
    {
        public GameStatusRepository(ApplicationContext context) : base(context)
        {
        }
    }
}