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
            _context.Entry(entity).Reference(e => e.ChessGameInfo).Load();
            return entity;
        }

        public override IEnumerable<Game> GetAll()
        {
            foreach (var entity in base.GetAll())
            {
                _context.Entry(entity).Reference(e => e.ChessGameInfo).Load();
                yield return entity;
            }
        }
    }
}