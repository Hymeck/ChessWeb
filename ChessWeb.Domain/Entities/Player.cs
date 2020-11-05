namespace ChessWeb.Domain.Entities
{
    public class Player : BaseEntity
    {
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}