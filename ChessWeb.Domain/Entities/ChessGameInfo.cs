namespace ChessWeb.Domain.Entities
{
    public class ChessGameInfo : BaseEntity 
    // public class ChessGameInfo
    {
        public bool HasWhiteKingMoved { get; set; }
        public bool HasBlackKingMoved { get; set; }

        public bool HasWhiteQueensideRookMoved { get; set; }
        public bool HasWhiteKingsideRookMoved { get; set; }

        public bool HasBlackQueensideRookMoved { get; set; }
        public bool HasBlackKingsideRookMoved { get; set; }

        public string WhiteKingSquare { get; set; }
        public string BlackKingSquare { get; set; }
        
        /// <summary>
        /// w_k b_k, w_q_r w_k_r, b_q_r b_k_r, w_s b_s
        /// </summary>
        public override string ToString()
        {
            var movedKings = $"{FromBool(HasWhiteKingMoved)}{FromBool(HasBlackKingMoved)}";
            var whiteRooks = $"{FromBool(HasWhiteQueensideRookMoved)}{FromBool(HasWhiteKingsideRookMoved)}";
            var blackRooks = $"{FromBool(HasBlackQueensideRookMoved)}{FromBool(HasBlackKingsideRookMoved)}";
            return $"{movedKings} {whiteRooks} {blackRooks} {WhiteKingSquare}_{BlackKingSquare}";
        }

        public ChessGameInfo(bool hasWhiteKingMoved, bool hasBlackKingMoved, bool hasWhiteQueensideRookMoved, bool hasWhiteKingsideRookMoved, bool hasBlackQueensideRookMoved, bool hasBlackKingsideRookMoved, string whiteKingSquare, string blackKingSquare)
        {
            HasWhiteKingMoved = hasWhiteKingMoved;
            HasBlackKingMoved = hasBlackKingMoved;
            HasWhiteQueensideRookMoved = hasWhiteQueensideRookMoved;
            HasWhiteKingsideRookMoved = hasWhiteKingsideRookMoved;
            HasBlackQueensideRookMoved = hasBlackQueensideRookMoved;
            HasBlackKingsideRookMoved = hasBlackKingsideRookMoved;
            WhiteKingSquare = whiteKingSquare;
            BlackKingSquare = blackKingSquare;
        }

        private char FromBool(bool value) =>
            value ? '+' : '-';
    }
}