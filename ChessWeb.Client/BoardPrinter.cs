using System.Text;

namespace ChessWeb.Client
{
    internal sealed class BoardPrinter : IBoardPrinter
    {
        private const char nonPieceFiller = '.';
        public string[] GetBoard(string fen)
        {
            var fenPieces = fen.Split()[0];
            var data = new StringBuilder(fenPieces);
            for (var j = 8; j >= 2; j--)
                data = data.Replace(j.ToString(), (j - 1).ToString() + '1');
            data = data.Replace('1', nonPieceFiller);
            var rows = data.ToString().Split('/');
            return rows;
        }
    }
}