using System;
using System.Threading.Tasks;

namespace ChessWeb.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IChessGameInfoRepository ChessGameInfos { get; set; }
        IColorRepository Colors { get; }
        IGameRepository Games { get; }
        IMoveRepository Moves { get; }
        IPlayerRepository Players { get; }
        ISideRepository Sides { get; }
        Task<int> Complete();
    }
}