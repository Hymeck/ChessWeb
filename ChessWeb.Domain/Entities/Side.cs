namespace ChessWeb.Domain.Entities
{
    public class Side : BaseEntity
    {
        public Game Game { get; set; }
        public Player Player { get; set; }
        public Color Color { get; set; }
    }
}