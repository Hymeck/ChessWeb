using System.Threading.Tasks;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Persistence.Contexts;

namespace ChessWeb.Persistence.Implementations
{
    public class GameStatusRepository : GenericRepository<GameStatus>, IGameStatusRepository
    {
        public GameStatusRepository(ApplicationDbContext dbContext) : base(dbContext) {}
        
        public async Task<GameStatus> Wait() => 
            await FindAsync(1);

        public async Task<GameStatus> Play() =>
            await FindAsync(2);

        public async Task<GameStatus> Draw() =>
            await FindAsync(3);

        public async Task<GameStatus> WhiteWon() =>
            await FindAsync(4);

        public async Task<GameStatus> BlackWon() =>
            await FindAsync(5);

        public async Task<GameStatus> Undefined() =>
            await FindAsync(6);
    }
}