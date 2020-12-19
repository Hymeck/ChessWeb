using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ChessWeb.Client;
using static System.Console;

namespace ChessWeb.Playground.Client
{
    class Program
    {
        private static string hymeckUsername = "Hymeck";
        private static string hymeckPassword = "hymeckpass";
        static async Task<int> Main(string[] args)
        {
            var program = new Program();
            // await program.MainCycle();
            program.Start();
            //todo: uncomment when finish
            // ReadKey();
            return 0;
        }

        public async Task MainCycle()
        {
            WriteLine("Даровень. Занесло в консоль клиент ChessWeb.");
            labelInput:
            WriteLine("1: Войти в существующую учетную запись.");
            WriteLine("2: Выйти.");
            var input = ReadLine();
            int.TryParse(input, out var choice);
            if (choice == 2)
            {
                WriteLine("Передайте парашют выходящему из окна нажатием на любую клавишу.");
                return;
            }

            if (choice != 1)
            {
                WriteLine("Пж, 1 либо 2.");
                goto labelInput;
            }
            
            Clear();

            var username = "";
            var password = "";

            ChessClient client;
            while (true)
            {
                Write("Погоняло: ");
                username = ReadLine();
                Write("Пароль: ");
                InputPassword(out password);
                WriteLine();
                try
                {
                    client = new ChessClient(username, password);
                    break;
                }
                catch (WebException)
                {
                    WriteLine("Пж, неверные вводы либо учтная запись в небытие.");
                }
            }
            
            WriteLine("Проникновение произошло успешно.");
            Write("Загрузка ");
            for (var i = 0; i < 3; i++)
            {
                await Task.Delay(1000);
                Write('.');
            }

            await Task.Delay(1000);
            
            WriteLine();
            Clear();
            
            WriteLine("Передайте парашют выходящему из окна нажатием на любую клавишу.");
            
            WriteLine(client.LastActiveGame);
        }

        private void InputPassword(out string password)
        {
            var sb = new StringBuilder("");
            while (true)
            {
                var key = ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                if (key.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length == 0)
                        continue;
                    sb = sb.Remove(sb.Length - 1, 1);
                }
                else 
                    sb.Append(key.KeyChar);
            }

            password = sb.ToString();
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
            // PrintBoard();
            PrintGames();
        }

        private void PrintGames()
        {
            var client = new ChessClient(hymeckUsername, hymeckPassword);
            WriteLine(client.LastActiveGame);
        }

        private void PrintBoard()
        {
            var client = new ChessClient(hymeckUsername, hymeckPassword);

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
            var client = new ChessClient(hymeckUsername, hymeckPassword);
            WriteLine(client.Join("2", true));
        }

        private void PrintLastGame()
        {
            var client = new ChessClient(hymeckUsername, hymeckPassword);
            WriteLine(client.GetLastGame());
        }

        private void PrintCreateGame()
        {
            var client = new ChessClient(hymeckUsername, hymeckPassword);
            var game = client.CreateGame();
            WriteLine(game);
        }

        private void Look()
        {
            var client = new ChessClient(hymeckUsername, hymeckPassword);
            var info = client.GetGame("1");
            WriteLine(info);
        }
        
        private void HymeckAndRacotyScholarsMate(string gameId = "1")
        {
            var hymeckClient = new ChessClient(hymeckUsername, hymeckPassword);
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