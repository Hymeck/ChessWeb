using System.Threading.Tasks;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Persistence.Contexts;

namespace ChessWeb.Persistence.Implementations
{
    public class GameStatusRepository : GenericRepository<GameStatus>, IGameStatusRepository
    {
        public GameStatusRepository(ApplicationDbContext dbContext) : base(dbContext) {}
        
        public async Task<GameStatus> WaitStatus() => 
            await FindAsync(1);

        public async Task<GameStatus> PlayStatus() =>
            await FindAsync(2);

        public async Task<GameStatus> DrawStatus() =>
            await FindAsync(3);

        public async Task<GameStatus> WhiteWonStatus() =>
            await FindAsync(4);

        public async Task<GameStatus> BlackWonStatus() =>
            await FindAsync(5);

        public async Task<GameStatus> UndefinedStatus() =>
            await FindAsync(6);
    }
}