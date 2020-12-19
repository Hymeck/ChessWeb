using System.Threading.Tasks;
using ChessWeb.Client;
using static System.Console;

namespace ChessWeb.Playground.Client
{
    class ChessConsoleClient
    {
        static async Task<int> Main(string[] args)
        {
            var gameLoop = new GameLoop();
            await gameLoop.GameCycle();
            ReadKey();
            return 0;
        }
        
        private static void HymeckAndRacotyScholarsMate(string gameId = "1")
        {
            var hymeckClient = new ChessClient("Hymeck", "hymeckpass");
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