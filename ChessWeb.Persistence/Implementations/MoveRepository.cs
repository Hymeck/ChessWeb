using System.Collections.Generic;
using System.Threading.Tasks;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Persistence.Contexts;

namespace ChessWeb.Persistence.Implementations
{
    public class MoveRepository : GenericRepository<Move>, IMoveRepository
    {
        public MoveRepository(ApplicationContext context) : base(context)
        {
        }

        public override Move Get(long id)
        {
            var entity = base.Get(id);
            _context.Entry(entity).Reference(e => e.Game).Load();
            _context.Entry(entity).Reference(e => e.Player).Load();
            return entity;
        }

        public override IEnumerable<Move> GetAll()
        {
            foreach (var entity in base.GetAll())
            {
                _context.Entry(entity).Reference(e => e.Game).Load();
                _context.Entry(entity).Reference(e => e.Player).Load();
                yield return entity;
            }
        }
    }
}