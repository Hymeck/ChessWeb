using System.Collections.Generic;
using System.Threading.Tasks;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces;
using ChessWeb.Persistence.Contexts;

namespace ChessWeb.Persistence.Implementations
{
    public class SideRepository : GenericRepository<Side>, ISideRepository
    {
        public SideRepository(ApplicationContext context) : base(context)
        {
        }

        public override async Task<Side> Get(int id)
        {
            var entity = await base.Get(id);
            await _context.Entry(entity).Reference(e => e.Game).LoadAsync();
            await _context.Entry(entity).Reference(e => e.Player).LoadAsync();
            await _context.Entry(entity).Reference(e => e.Color).LoadAsync();
            return entity;
        }

        public override IEnumerable<Side> GetAll()
        {
            foreach (var entity in base.GetAll())
            {
                _context.Entry(entity).Reference(e => e.Game).Load();
                _context.Entry(entity).Reference(e => e.Player).Load();
                _context.Entry(entity).Reference(e => e.Color).Load();
                yield return entity;
            }
        }
    }
}