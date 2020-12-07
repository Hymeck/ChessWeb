using ChessWeb.Domain.Entities;

namespace ChessWeb.Service.Interfaces
{
    public interface IChessGameService
    {
        Game MakeMove(Game game, Move move, Side side);
        void AddToGame(User user, Game game, Color color);
    }
}