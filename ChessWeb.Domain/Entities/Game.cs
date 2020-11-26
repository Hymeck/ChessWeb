using System;

namespace ChessWeb.Domain.Entities
{
    public class Game : BaseEntity
    {
        public string Fen { get; set; }
        public virtual ChessGameInfo ChessGameInfo { get; set; }
    }
}