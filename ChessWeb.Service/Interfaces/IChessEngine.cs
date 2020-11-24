using ChessWeb.Domain.Entities;

namespace ChessWeb.Service.Interfaces
{
    public interface IChessEngine
    {
        Game MakeMove(Game game, Move move);
    }
}