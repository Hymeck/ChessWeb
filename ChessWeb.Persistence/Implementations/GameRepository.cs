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
                .Include(e => e.WhiteUser)
                .Include(e => e.BlackUser)
                .OrderBy(e => e.Id).ToListAsync();

        public async Task<IEnumerable<Game>> GetUserGamesAsync(User user) =>
            await _dbContext.Games
                .Where(x => x.WhiteUser == user || x.BlackUser == user)
                .OrderBy(x => x.Id)
                .ToListAsync();

        public async Task CreateAsync()
        {
            var whiteColor = await _dbContext.Colors.FindAsync(1L);
            var game = Game.StartGame();
            await _dbContext.Games.AddAsync(game);
            var blackColor = await _dbContext.Colors.FindAsync(2L);
            var whiteSide = new Side {Color = whiteColor, Game = game};
            var blackSide = new Side {Color = blackColor, Game = game};
            await _dbContext.Sides.AddRangeAsync(whiteSide, blackSide);
            await SaveChangesAsync();
        }

        public async Task<Game> GetAsync(long id)
        {
            var entity = await FindAsync(id);
            await _dbContext.Entry(entity).Reference(e => e.WhiteUser).LoadAsync();
            await _dbContext.Entry(entity).Reference(e => e.BlackUser).LoadAsync();
            return entity;
        }

        public async Task DeleteAsync(long id)
        {
            var game = await FindAsync(id);
            Delete(game);
        }

        public bool Any() => 
            _dbContext.Games.Any();
    }
}