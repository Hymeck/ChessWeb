using System;

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
    }
}