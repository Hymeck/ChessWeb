namespace ChessWeb.Domain.Entities
{
    public class Moves : BaseEntity
    {
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public string Fen { get; set; }
        public string MoveNext { get; set; }
    }
}