using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Persistence.Contexts;

namespace ChessWeb.Persistence.Implementations
{
    public class GameSummaryRepository : GenericRepository<GameSummary>, IGameSummaryRepository
    {
        public GameSummaryRepository(ApplicationContext context) : base(context)
        {
        }

        public override GameSummary Get(long id)
        {
            var entity = base.Get(id);
            _context.Entry(entity).Reference(e => e.Game).Load();
            _context.Entry(entity).Reference(e => e.Status).Load();
            _context.Entry(entity).Reference(e => e.WhiteUser).Load();
            _context.Entry(entity).Reference(e => e.BlackUser).Load();
            _context.Entry(entity).Reference(e => e.Winner).Load();
            _context.Entry(entity).Reference(e => e.ActiveColor).Load();
            return entity;
        }
    }
}