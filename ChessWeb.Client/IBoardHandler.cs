using System;

namespace ChessWeb.Client
{
    public interface IBoardHandler
    {
        string[] GetBoard(string fen);
        void PrintBoard(string[] rows, bool isWhiteSide, Action<string> printer);
    }
}