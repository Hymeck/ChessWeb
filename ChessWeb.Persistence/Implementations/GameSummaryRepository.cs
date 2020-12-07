using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Persistence.Contexts;

namespace ChessWeb.Persistence.Implementations
{
    public class GameSummaryRepository : GenericRepository<GameSummary>, IGameSummaryRepository
    {
        public GameSummaryRepository(ApplicationDbContext dbContext) : base(dbContext) {}
        
    }
}