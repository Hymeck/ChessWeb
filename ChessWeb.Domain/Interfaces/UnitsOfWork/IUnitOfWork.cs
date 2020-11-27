using System;
using System.Threading.Tasks;
using ChessWeb.Domain.Interfaces.Repositories;

namespace ChessWeb.Domain.Interfaces.UnitsOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IChessGameInfoRepository ChessGameInfos { get; set; }
        IColorRepository Colors { get; }
        IGameRepository Games { get; }
        IMoveRepository Moves { get; }
        IPlayerRepository Players { get; }
        ISideRepository Sides { get; }
        int Complete();
    }
}