using System;
using ChessWeb.Client;
using static System.Console;

namespace ChessWeb.Playground.Client
{
    class Program
    {
        // private static readonly string host = "http://localhost:5000/api/games/";
        private static readonly string host = "http://chessbsuir.herokuapp.com/api/games/";
        private static readonly string user = "Hymeck";
        static void Main(string[] args)
        {
            var program = new Program();
            program.Start();
            ReadKey();
        }

        private void Start()
        {
            // HymeckAndRacotyScholarsMate();
            Look();
        }

        private void Look()
        {
            var client = new ChessClient(host, user);
            var info = client.GetCurrentGame("1");
            WriteLine(info);
        }
        
        private void HymeckAndRacotyScholarsMate()
        {
            var hymeckClient = new ChessClient(host, user);
            var racotyClient = new ChessClient(host, "Racoty");
            WriteLine(hymeckClient.MakeMove("1", "e2e4"));
            WriteLine("\n");
            WriteLine(racotyClient.MakeMove("1", "e7e5"));
            WriteLine("\n");
            WriteLine(hymeckClient.MakeMove("1", "d1h5"));
            WriteLine("\n");
            WriteLine(racotyClient.MakeMove("1", "b8c6"));
            WriteLine("\n");
            WriteLine(hymeckClient.MakeMove("1", "f1c4"));
            WriteLine("\n");
            WriteLine(racotyClient.MakeMove("1", "g8f6"));
            WriteLine("\n");
            WriteLine(hymeckClient.MakeMove("1", "h5f7"));
        }
    }
}