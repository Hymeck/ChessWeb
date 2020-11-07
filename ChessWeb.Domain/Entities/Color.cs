namespace ChessWeb.Domain.Entities
{
    public class Color : BaseEntity
    {
        public bool ColorType { get; set; }

        public override string ToString() =>
            ColorType ? "White" : "Black";
    }
}