using ChessEngine;
using ChessWeb.Domain.Entities;
using ChessWeb.Service.Interfaces;

namespace ChessWeb.Service.Services
{
    public class ChessEngine : IChessEngine
    {
        public Game MakeMove(Game game, Move move)
        {
            ChessGame chessGame = new ChessGame();
            return null;
        }
    }
}