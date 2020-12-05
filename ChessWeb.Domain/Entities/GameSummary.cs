namespace ChessWeb.Domain.Entities
{
    public class GameSummary : BaseEntity
    {
        public long GameId { get; set; }
        public Game Game { get; set; }
        public string Fen { get; set; }
        public string Status { get; set; }
        public string WhiteUser { get; set; }
        public string BlackUser { get; set; }
        public string LastMove { get; set; }
        public string ActiveColor { get; set; }
        public string Winner { get; set; }

        public GameSummary(Game game, GameStatus status, Color color)
        {
            Game = game;
            Fen = game.Fen;
            Status = status.ToString();
            ActiveColor = color.ToString();
        }

        public GameSummary() { }
    }
}