using System;
using System.Threading.Tasks;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Domain.Interfaces.UnitsOfWork;
using ChessWeb.Persistence.Contexts;

namespace ChessWeb.Persistence.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public IChessGameInfoRepository ChessGameInfos { get; set; }
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
            ChessGameInfos = new ChessGameInfoRepository(_context);
            Colors = new ColorRepository(_context);
            Players = new PlayerRepository(_context);
            Moves = new MoveRepository(_context);
            Games = new GameRepository(_context);
            Sides = new SideRepository(_context);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) { if (disposing) _context.Dispose(); }

        public int Complete() =>
            _context.SaveChanges();
    }
}