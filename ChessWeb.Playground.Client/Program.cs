using System;
using ChessWeb.Client;
using static System.Console;

namespace ChessWeb.Playground.Client
{
    class Program
    {
        private static readonly string host = "http://localhost:5000/api/games/";
        private static readonly string user = "Hymeck";
        static void Main(string[] args)
        {
            var program = new Program();
            program.Start();
            ReadKey();
        }

        private void Start()
        {
            var client = new ChessClient(host, user);
            WriteLine(client.MakeMove("1", "Hymeck", "e2e4"));
            WriteLine("\n");
            WriteLine(client.MakeMove("1", "Racoty", "e7e5"));
            WriteLine("\n");
            WriteLine(client.MakeMove("1", "Hymeck", "d1h5"));
            WriteLine("\n");
            WriteLine(client.MakeMove("1", "Racoty", "b8c6"));
            WriteLine("\n");
            WriteLine(client.MakeMove("1", "Hymeck", "f1c4"));
            WriteLine("\n");
            WriteLine(client.MakeMove("1", "Racoty", "g8f6"));
            WriteLine("\n");
            WriteLine(client.MakeMove("1", "Hymeck", "h5f7"));
        }
    }
}