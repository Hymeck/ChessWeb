using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using static System.Console;

namespace ChessWeb.Client
{
    public class ChessClient
    {
        public readonly string Host;
        public readonly string User;

        private const string pattern = @"""(\w+)\"":""?([^,""}]*)""?";
        
        public ChessClient(string host, string user)
        {
            Host = host;
            User = user;
        }

        public GameInfo GetCurrentGame() => 
            new(ParseJson(CallServer()));

        public GameInfo MakeMove(string gameId, string username, string move) =>
            new(ParseJson(CallServer($"{gameId}/{username}/{move}")));
        private string CallServer(string param = "")
        {
            // var request = WebRequest.Create(Host + User + "/" + param);
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