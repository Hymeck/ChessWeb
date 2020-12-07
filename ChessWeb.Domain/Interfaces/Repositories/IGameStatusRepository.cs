using System.Threading.Tasks;
using ChessWeb.Domain.Entities;

namespace ChessWeb.Domain.Interfaces.Repositories
{
    public interface IGameStatusRepository : IGenericRepository<GameStatus>
    {
        Task<GameStatus> Wait();
        Task<GameStatus> Play();
        Task<GameStatus> Draw();
        Task<GameStatus> WhiteWon();
        Task<GameStatus> BlackWon();
        Task<GameStatus> Undefined();
    }
}