namespace ChessWeb.Domain.Entities
{
    public class GameStatus : BaseEntity
    {
        /// <summary>
        /// 0 - wait; 1 - play; 2 - stalemate; 3 - white won; 4 - black won, other - undefined
        /// </summary>
        public byte Status { get; set; }

        public override string ToString() =>
            Status switch
            {
                0 => "wait",
                1 => "play",
                2 => "draw",
                3 => "ww",
                4 => "bw",
                _ => "undefined"
            };
    }
}