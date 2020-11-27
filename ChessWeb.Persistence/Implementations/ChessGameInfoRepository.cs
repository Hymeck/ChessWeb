using System.Collections.Generic;
using System.Threading.Tasks;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces;
using ChessWeb.Persistence.Contexts;

namespace ChessWeb.Persistence.Implementations
{
    public class ChessGameInfoRepository : GenericRepository<ChessGameInfo>, IChessGameInfoRepository
    {
        public ChessGameInfoRepository(ApplicationContext context) : base(context)
        {
        }

        public override async Task<ChessGameInfo> Get(int id)
        {
            var entity = await base.Get(id);
            await _context.Entry(entity).Reference(e => e.Game).LoadAsync();
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