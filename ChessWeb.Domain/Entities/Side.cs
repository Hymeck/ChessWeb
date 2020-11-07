namespace ChessWeb.Domain.Entities
{
    public class Side : BaseEntity
    {
        public long GameId { get; set; }
        public long PlayerId { get; set; }
        public long ColorId { get; set; }
    }
}