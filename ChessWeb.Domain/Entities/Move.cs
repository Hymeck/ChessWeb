namespace ChessWeb.Domain.Entities
{
    public class Move : BaseEntity
    {
        public Game Game { get; set; }
        public Player Player { get; set; }
        public string Fen { get; set; }
        public string MoveNext { get; set; }
    }
}