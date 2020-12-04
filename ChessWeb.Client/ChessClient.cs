using System;
using System.IO;
using System.Net;

namespace ChessWeb.Client
{
    public class ChessClient
    {
        public readonly string Host;
        public readonly string User;

        public ChessClient(string host, string user)
        {
            Host = host;
            User = user;
        }

        public void GetCurrentGame()
        {
            Console.WriteLine(CallServer());
        }

        private string CallServer(string param = "")
        {
            // var request = WebRequest.Create(Host + User + "/" + param);
            var request = WebRequest.Create(Host);
            var response = request.GetResponse();
            using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}