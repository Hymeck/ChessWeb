using System.Threading.Tasks;
using ChessWeb.Domain.Entities;

namespace ChessWeb.Domain.Interfaces.Repositories
{
    public interface IGameStatusRepository : IGenericRepository<GameStatus>
    {
        Task<GameStatus> WaitStatus();
        Task<GameStatus> PlayStatus();
        Task<GameStatus> DrawStatus();
        Task<GameStatus> WhiteWonStatus();
        Task<GameStatus> BlackWonStatus();
        Task<GameStatus> UndefinedStatus();
    }
}