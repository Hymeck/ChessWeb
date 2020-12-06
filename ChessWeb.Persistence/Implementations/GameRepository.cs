using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ChessWeb.Persistence.Implementations
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        public GameRepository(ApplicationDbContext dbContext) : base(dbContext) {}

        public async Task<IEnumerable<Game>> GetAllAsync() => 
            await _dbContext.Games
                .AsNoTracking()
                .Include(e => e.GameSummary)
                .Include(e => e.WhiteUser)
                .Include(e => e.BlackUser)
                .OrderBy(e => e.Id).ToListAsync();

        public async Task<IEnumerable<Game>> GetUserGamesAsync(User user) =>
            (await GetAllAsync()).Where(x =>
                x.WhiteUser?.UserName == user.UserName || 
                x.BlackUser?.UserName == user.UserName);

        public async Task CreateGameAsync()
        {
            var whiteColor = await _dbContext.Colors.FindAsync(1L);
            var gameStatus = await _dbContext.GameStatuses.FindAsync(1L);
            var game = new Game();
            var gameSummary = new GameSummary(game, gameStatus, whiteColor);
            game.GameSummary = gameSummary;
            await AddAsync(game);
            var blackColor = await _dbContext.Colors.FindAsync(2L);
            var whiteSide = new Side {Color = whiteColor, Game = game};
            var blackSide = new Side {Color = blackColor, Game = game};
            await _dbContext.Sides.AddAsync(whiteSide);
            await _dbContext.Sides.AddAsync(blackSide);
            await SaveChangesAsync();
        }

        public async Task<Game> GetAsync(long id)
        {
            var entity = await FindAsync(id);
            await _dbContext.Entry(entity).Reference(e => e.WhiteUser).LoadAsync();
            await _dbContext.Entry(entity).Reference(e => e.BlackUser).LoadAsync();
            await _dbContext.Entry(entity).Reference(e => e.GameSummary).LoadAsync();
            return entity;
        }

        public Task JoinAsync(User user, Side side)
        {
            throw new NotImplementedException(nameof(JoinAsync));
        }
    }
}