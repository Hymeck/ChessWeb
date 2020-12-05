using System.ComponentModel.DataAnnotations;

namespace ChessWeb.Domain.Entities
{
    public class GameSummary : BaseEntity
    {
        public long GameId { get; set; }
        public Game Game { get; set; }
        public byte Status { get; set; }
        [MaxLength(5)]
        public string LastMove { get; set; }
        public bool ActiveColor { get; set; }
        [MaxLength(30)]
        public string Winner { get; set; }

        public GameSummary(Game game, GameStatus status, Color color)
        {
            Game = game;
            Status = status.Status;
            ActiveColor = color.ColorType;
        }

        public GameSummary() { }
    }
}