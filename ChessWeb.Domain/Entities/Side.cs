namespace ChessWeb.Domain.Entities
{
    public class Side : BaseEntity
    {
        public long GameId { get; set; }
        public Game Game { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public long ColorId { get; set; }
        public Color Color { get; set; }
    }
}