using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using static System.Console;

namespace ChessWeb.Client
{
    public sealed class ChessClient
    {
        public readonly string Host;
        public readonly string Username;
        private readonly IBoardPrinter _boardPrinter;

        private const string pattern = @"""(\w+)\"":""?([^,""}]*)""?";
        
        public ChessClient(string host, string username)
        {
            Host = host;
            Username = username;
            _boardPrinter = new BoardPrinter();
        }

        public GameInfo GetCurrentGame(string gameId) => 
            new(ParseJson(CallServer(gameId)));

        public void PrintBoard(GameInfo gameInfo, bool isWhiteSide = true)
        {
            var rows = _boardPrinter.GetBoard(gameInfo.fen);
            if (isWhiteSide)
            {
                var y = 7;
                foreach (var row in rows)
                {
                    PrintRow(row, y);
                    y--;
                }
            }

            else
            {
                var y = 0;
                for (var i = rows.Length - 1; i >= 0; i--)
                {
                    PrintRow(rows[i], y);
                    y++;
                }
                // foreach (var row in rows)
                // {
                //     PrintRow(row, y);
                //     y++;
                // }
            }
            WriteLine(" a b c d e f g h\n");
        }

        private void PrintRow(string row, int y)
        {
            foreach (var p in row) 
                Write($" {p}");
            WriteLine($" {y + 1}");
        }
        
        public GameInfo MakeMove(string gameId, string move) =>
            new(ParseJson(CallServer($"{gameId}/{Username}/{move}")));
        private string CallServer(string param = "")
        {
            var request = WebRequest.Create(Host + param);
            var response = request.GetResponse();
            using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        private NameValueCollection ParseJson(string json)
        {
            var list = new NameValueCollection();

            foreach (Match m in Regex.Matches(json, pattern))
                if (m.Groups.Count == 3)
                    list[m.Groups[1].Value] = m.Groups[2].Value;
            
            return list;
        }
    }
}