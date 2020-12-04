namespace ChessWeb.Domain.Entities
{
    public class GameSummary : BaseEntity
    {
        public long GameId { get; set; }
        public Game Game { get; set; }
        public string Fen { get; set; }
        public GameStatus Status { get; set; }
        public User WhiteUser { get; set; }
        public User BlackUser { get; set; }
        public string LastMove { get; set; }
        public Color ActiveColor { get; set; }
        public User Winner { get; set; }

        public GameSummary(Game game, GameStatus status, Color color)
        {
            Game = game;
            Fen = game.Fen;
            Status = status;
            ActiveColor = color;
        }

        public GameSummary() { }
    }
}