using System.Collections.Specialized;

namespace ChessWeb.Client
{
    public class GameInfo
    {
        public long gameId;
        public string fen;
        public byte status;
        public string whiteUsername;
        public string blackUsername;
        public string lastMove;
        public string activeColor;
        // public string YourColor;
        // public bool IsYourMove;
        public string winner;

        public GameInfo(NameValueCollection list)
        {
            gameId = long.Parse(list[nameof(gameId)]);
            fen = list[nameof(fen)];
            status = byte.Parse(list[nameof(status)]);
            whiteUsername = list[nameof(whiteUsername)];
            blackUsername = list[nameof(blackUsername)];
            lastMove = list[nameof(lastMove)];
            activeColor = list[nameof(activeColor)];
            winner = list[nameof(winner)];
        }

        public override string ToString() =>
            $"{nameof(gameId)}: {gameId}\n" +
            $"{nameof(fen)}: {fen}\n" +
            $"{nameof(status)}: {status}\n" +
            $"{nameof(whiteUsername)}: {whiteUsername}\n" +
            $"{nameof(blackUsername)}: {blackUsername}\n" +
            $"{nameof(lastMove)}: {lastMove}\n" +
            $"{nameof(activeColor)}: {activeColor}\n" +
            $"{nameof(winner)}: {winner}\n";
    }
}