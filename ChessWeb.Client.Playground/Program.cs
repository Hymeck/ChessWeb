using System;
using ChessWeb.Client;
using static System.Console;

namespace ChessWeb.Playground.Client
{
    class Program
    {
        private static string user = "Hymeck";
        private static string password = "hymeckpass";
        static void Main(string[] args)
        {
            var program = new Program();
            program.Start();
            ReadKey();
        }

        private void Start()
        {
            // JoinRacoty();
            // HymeckAndRacotyScholarsMate("2");
            // Look();
            // PrintLastGame();
            // PrintLastGame();
            // PrintCreateGame();
            // PrintJoinGame();
            PrintBoard();
        }

        private void PrintBoard()
        {
            var client = new ChessClient(user, password);

            var game = client.GetGame("2");
            var rows = client.BoardHandler.GetBoard(game.fen);
            client.BoardHandler.PrintBoard(rows, true, Write);
        }
        
        private void JoinRacoty()
        {
            var racotyClient = new ChessClient("Racoty", "racotypass");
            racotyClient.Join("2", false);
        }

        private void PrintJoinGame()
        {
            var client = new ChessClient(user, password);
            WriteLine(client.Join("2", true));
        }

        private void PrintLastGame()
        {
            var client = new ChessClient(user, password);
            WriteLine(client.GetLastGame());
        }

        private void PrintCreateGame()
        {
            var client = new ChessClient(user, password);
            var game = client.CreateGame();
            WriteLine(game);
        }

        private void Look()
        {
            var client = new ChessClient(user, password);
            var info = client.GetGame("1");
            WriteLine(info);
        }
        
        private void HymeckAndRacotyScholarsMate(string gameId = "1")
        {
            var hymeckClient = new ChessClient(user, password);
            var racotyClient = new ChessClient("Racoty", "racotypass");
            WriteLine(hymeckClient.MakeMove(gameId, "e2e4"));
            WriteLine("\n");
            WriteLine(racotyClient.MakeMove(gameId, "e7e5"));
            WriteLine("\n");
            WriteLine(hymeckClient.MakeMove(gameId, "d1h5"));
            WriteLine("\n");
            WriteLine(racotyClient.MakeMove(gameId, "b8c6"));
            WriteLine("\n");
            WriteLine(hymeckClient.MakeMove(gameId, "f1c4"));
            WriteLine("\n");
            WriteLine(racotyClient.MakeMove(gameId, "g8f6"));
            WriteLine("\n");
            WriteLine(hymeckClient.MakeMove(gameId, "h5f7"));
        }
    }
}