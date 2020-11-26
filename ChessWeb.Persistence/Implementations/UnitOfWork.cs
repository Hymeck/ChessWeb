using System;
using System.Threading.Tasks;
using ChessWeb.Domain.Interfaces;
using ChessWeb.Persistence.Contexts;

namespace ChessWeb.Persistence.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public IColorRepository Colors { get; }
        public IGameRepository Games { get; }
        public IMoveRepository Moves { get; }
        public IPlayerRepository Players { get; }
        public ISideRepository Sides { get; }

        public UnitOfWork(ApplicationContext context)
        {
            // todo: add other repos
            _context = context;
            // todo: remove tight coupling in later
            Colors = new ColorRepository(_context);
            Players = new PlayerRepository(_context);
            Moves = new MoveRepository(_context);
            Games = new GameRepository(_context);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) { if (disposing) _context.Dispose(); }

        public async Task<int> Complete() =>
            await _context.SaveChangesAsync();
    }
}