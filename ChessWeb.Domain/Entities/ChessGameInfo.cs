namespace ChessWeb.Domain.Entities
{
    public class ChessGameInfo : BaseEntity
    {
        public bool HasWhiteKingMoved {get; set;}
        public bool HasBlackKingMoved {get; set;}

        public bool HasWhiteQueensideRookMoved {get; set;}
        public bool HasWhiteKingsideRookMoved {get; set;}

        public bool HasBlackQueensideRookMoved {get; set;}
        public bool HasBlackKingsideRookMoved {get; set;}

        public string WhiteKingSquare {get; set;}
        public string BlackKingSquare {get; set;}
    }
}