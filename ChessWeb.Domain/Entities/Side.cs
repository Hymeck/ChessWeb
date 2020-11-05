namespace ChessWeb.Domain.Entities
{
    public class Side : BaseEntity
    {
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public int ColorId { get; set; }
    }
}