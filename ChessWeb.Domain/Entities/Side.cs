namespace ChessWeb.Domain.Entities
{
    public class Side : BaseEntity
    {
        public virtual Game Game { get; set; }
        public virtual User User { get; set; }
        public virtual Color Color { get; set; }
    }
}