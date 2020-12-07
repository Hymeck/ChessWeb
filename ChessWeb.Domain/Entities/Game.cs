using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChessWeb.Domain.Entities
{
    public class Game : BaseEntity
    {
        [MaxLength(100)]
        public string Fen { get; set; } = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        public string WhiteUserId { get; set; }
        public User WhiteUser { get; set; }
        public string BlackUserId { get; set; }
        public User BlackUser { get; set; }
        public long GameSummaryId { get; set; }
        public GameSummary GameSummary { get; set; }

        public Game()
        {
        }
    }
}