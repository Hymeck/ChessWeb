namespace ChessWeb.Domain.Entities
{
    public class Move : BaseEntity
    {
        public long GameId { get; set; }
        public Game Game { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string Fen { get; set; }
        public string MoveNext { get; set; }
    }
}