using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ChessWeb.Persistence.Implementations
{
    public class MoveRepository : GenericRepository<Move>, IMoveRepository
    {
        public MoveRepository(ApplicationDbContext dbContext) : base(dbContext) {}

        public async Task<IEnumerable<Move>> GetAllAsync() =>
            await _dbContext.Moves
                .AsNoTracking()
                .Include(x => x.User)
                .Include(x => x.Game)
                .OrderBy(x => x.Id)
                .ToListAsync();

        public async Task<IEnumerable<Move>> GetGameMoves(Game game) =>
            await _dbContext.Moves
                .Where(x => x.Game == game)
                .OrderBy(x => x.Id)
                .ToListAsync();
    }
}