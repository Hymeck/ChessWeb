using System.Collections.Generic;
using System.Threading.Tasks;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Persistence.Contexts;

namespace ChessWeb.Persistence.Implementations
{
    public class ChessGameInfoRepository : GenericRepository<ChessGameInfo>, IChessGameInfoRepository
    {
        public ChessGameInfoRepository(ApplicationContext context) : base(context)
        {
        }

        public override ChessGameInfo Get(long id)
        {
            var entity = base.Get(id);
            _context.Entry(entity).Reference(e => e.Game).Load();
            return entity;
        }

        public override IEnumerable<ChessGameInfo> GetAll()
        {
            foreach (var entity in base.GetAll())
            {
                _context.Entry(entity).Reference(e => e.Game).Load();
                yield return entity;
            }
        }
    }
}