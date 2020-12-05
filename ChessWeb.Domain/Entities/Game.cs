using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChessWeb.Domain.Entities
{
    public class Game : BaseEntity
    {
        [MaxLength(100)]
        public string Fen { get; set; } = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        public List<Side> Sides { get; set; }
        public GameSummary GameSummary { get; set; }
    }
}