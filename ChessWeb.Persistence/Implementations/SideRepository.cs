using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ChessWeb.Persistence.Implementations
{
    public class SideRepository : GenericRepository<Side>, ISideRepository
    {
        public SideRepository(ApplicationDbContext dbContext) : base(dbContext) {}
        public async Task<IEnumerable<Side>> GetGameSides(Game game)
        {
            var sides =  _dbContext.Sides.Where(x => x.Game == game).AsEnumerable();
            // foreach (var side in sides)
            // {
            //     await _dbContext.Entry(side).Reference(e => e.Color).LoadAsync();
            //     await _dbContext.Entry(side).Reference(e => e.User).LoadAsync();
            // }

            return sides;
        }

        public int GetActiveGamePlayerCount(Game game) =>
            _dbContext.Sides
                .Count(x => x.Game == game && x.UserId != null);
    }
}