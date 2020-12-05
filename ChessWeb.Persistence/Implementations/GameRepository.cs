using System.Collections.Generic;
using System.Threading.Tasks;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Persistence.Contexts;

namespace ChessWeb.Persistence.Implementations
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        public GameRepository(ApplicationContext context) : base(context)
        {
        }

        public override Game Get(long id)
        {
            var entity = base.Get(id);
            _context.Entry(entity).Reference(e => e.GameSummary).Load();
            // _context.Entry(entity).Reference(e => e.GameSummary.Status).Load();
            // _context.Entry(entity).Reference(e => e.GameSummary.ActiveColor).Load();
            // _context.Entry(entity).Reference(e => e.GameSummary.WhiteUser).Load();
            // _context.Entry(entity).Reference(e => e.GameSummary.BlackUser).Load();
            return entity;
        }

        public IEnumerable<Game> GetUserGames(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}