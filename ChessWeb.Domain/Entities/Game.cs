using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChessWeb.Domain.Entities
{
    public class Game : BaseEntity
    {
        [MaxLength(100)]
        public string Fen { get; set; } = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        public string WhiteUserId { get; set; }
        public User WhiteUser { get; set; }
        public string WhiteUsername { get; set; }
        public string BlackUserId { get; set; }
        public User BlackUser { get; set; }
        public string BlackUsername { get; set; }
        public long GameSummaryId { get; set; }
        public byte Status { get; set; }
        [NotMapped]
        public string VerbalStatus =>
            Status switch
            {
                0 => "Wait",
                1 => "Play",
                2 => "Draw",
                3 => "White won",
                4 => "Black Won",
                _ => "Undefined"
            };
        [MaxLength(5)]
        public string LastMove { get; set; }
        public bool ActiveColor { get; set; }
        [NotMapped]
        public string VerbalActiveColor =>
            ActiveColor ? "White" : "Black";
        [MaxLength(30)]
        public string Winner { get; set; }

        public static Game StartGame() => 
            new() {ActiveColor = true};

        public override string ToString() =>
            $"{WhiteUsername ?? "."} vs {BlackUsername ?? "."}; {Fen}";
    }
}