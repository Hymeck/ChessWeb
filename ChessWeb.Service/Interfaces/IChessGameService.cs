using System.Threading.Tasks;
using ChessWeb.Domain.Entities;

namespace ChessWeb.Service.Interfaces
{
    public interface IChessGameService
    {
        Task<Game> MakeMove(long gameId, string userId, string move);
        // Task<bool> Join(long gameId, string userId, Color color);
        // Task AddToGame(User user, Game game, Color color);
    }
}