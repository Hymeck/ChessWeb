namespace ChessWeb.Domain.Entities
{
    public class Move : BaseEntity
    {
        public virtual Game Game { get; set; }
        public virtual User User { get; set; }
        public string Fen { get; set; }
        public string MoveNext { get; set; }
    }
}