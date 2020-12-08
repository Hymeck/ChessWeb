using ChessWeb.Domain.Entities;

namespace ChessWeb.Application.DTO
{
    public class GameDto
    {
        public long GameId { get; set; }
        public string Fen { get; set; }
        /// <summary>
        /// 0 - wait; 1 - play; 2 - draw; 3 - white won; 4 - black won, other - undefined
        /// </summary>
        public byte Status { get; set; }
        public string WhiteUsername { get; set; }
        public string BlackUsername { get; set; }
        public string LastMove { get; set; }
        public string ActiveColor { get; set; }
        public string Winner { get; set; }

        public GameDto(Game game)
        {
            GameId = game.Id;
            Fen = game.Fen;
            Status = game.Status;
            WhiteUsername = game.WhiteUsername;
            BlackUsername = game.BlackUsername;
            LastMove = game.LastMove;
            ActiveColor = game.VerbalActiveColor;
            Winner = game.Winner;
        }
    }
}