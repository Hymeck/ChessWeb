namespace ChessWeb.Domain.Entities
{
    public class Move : BaseEntity
    {
        public long GameId { get; set; }
        public long PlayerId { get; set; }
        public string Fen { get; set; }
        public string MoveNext { get; set; }
    }
}