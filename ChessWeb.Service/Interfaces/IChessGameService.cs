using System.Threading.Tasks;
using ChessWeb.Domain.Entities;

namespace ChessWeb.Service.Interfaces
{
    public interface IChessGameService
    {
        Task<Game> MakeMove(long gameId, string username, string move);
        // Task AddToGame(User user, Game game, Color color);
    }
}