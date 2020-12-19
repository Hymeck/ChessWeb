using System.Collections.Specialized;

namespace ChessWeb.Client
{
    public class GameInfo
    {
        public readonly long gameId;
        public readonly string fen;
        public readonly byte status;
        public readonly string whiteUsername;
        public readonly string blackUsername;
        public readonly string lastMove;
        public readonly string activeColor;
        public readonly string winner;

        public GameInfo(NameValueCollection parsedJson)
        {
            gameId = long.Parse(parsedJson[nameof(gameId)]);
            fen = parsedJson[nameof(fen)];
            status = byte.Parse(parsedJson[nameof(status)]);
            whiteUsername = parsedJson[nameof(whiteUsername)];
            blackUsername = parsedJson[nameof(blackUsername)];
            lastMove = parsedJson[nameof(lastMove)];
            activeColor = parsedJson[nameof(activeColor)];
            winner = parsedJson[nameof(winner)];
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