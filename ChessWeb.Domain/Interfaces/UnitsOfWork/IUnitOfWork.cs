using System;
using System.Threading.Tasks;
using ChessWeb.Domain.Interfaces.Repositories;

namespace ChessWeb.Domain.Interfaces.UnitsOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IColorRepository Colors { get; }
        IGameRepository Games { get; }
        IMoveRepository Moves { get; }
        IPlayerRepository Players { get; }
        ISideRepository Sides { get; }
        IGameStatusRepository GameStatuses { get; }
        IGameSummaryRepository GameSummaries { get; }
        int Complete();
    }
}