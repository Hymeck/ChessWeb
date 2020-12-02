using System;

namespace ChessWeb.Domain.Entities
{
    public class Game : BaseEntity
    {
        public string Fen { get; set; }

        public Game() => 
            Fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    }
}