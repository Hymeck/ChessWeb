using System;
using System.Text;

namespace ChessWeb.Client
{
    internal sealed class BoardHandler : IBoardHandler
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

        public void PrintBoard(string[] rows, bool isWhiteSide, Action<string> printer)
        {
            if (isWhiteSide)
            {
                var y = 7;
                foreach (var row in rows)
                {
                    PrintRow(row, y, printer);
                    y--;
                }
            }

            else
            {
                var y = 0;
                for (var i = rows.Length - 1; i >= 0; i--)
                {
                    PrintRow(rows[i], y, printer);
                    y++;
                }
            }

            printer(" a b c d e f g h\n\n");
        }
        
        private void PrintRow(string row, int y, Action<string> printer)
        {
            foreach (var p in row)
                printer($" {p}");
            printer($" {y + 1}\n");
        }
    }
}