using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ChessWeb.Client;
using static System.Console;

namespace ChessWeb.Playground.Client
{
    public class GameLoop
    {
        public async Task GameCycle()
        {
            WriteLine("Даровень. Занесло в консольный клиент ChessWeb.");
            labelInput:
            WriteLine("1: Проникнуть в существующую учетную запись.");
            WriteLine("2: Уйти из зоопарка.");
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

            GameInfo lastActiveGame;
            var move = "";
            string userColor;
            bool isWhite;
            do
            {
                lastActiveGame = client.LastActiveUserGame;
                isWhite = lastActiveGame.whiteUsername == username;
                userColor = isWhite
                    ? "White"
                    : "Black";
                var rows = client.BoardHandler.GetBoard(lastActiveGame.fen);
                client.BoardHandler.PrintBoard(rows, isWhite, Write);
                
                if (lastActiveGame.status == 3)
                {
                    WriteLine($"Пж, игра завершена. Взобравшийся на голову: {lastActiveGame.winner}.");
                    break;
                }

                if (lastActiveGame.activeColor == userColor)
                {
                    Write("Выпад: ");
                    move = ReadLine();
                    
                    if (move == "q")
                    {
                        WriteLine("Досрочный уход из зоопарка.");
                        break;
                    }
                    
                    client.MakeMove(lastActiveGame.gameId.ToString(), move);
                }
                
                else
                {
                    WriteLine("Нет череда... Ввод клавиши для обновления экрана: ");
                    move = ReadLine();
                
                    if (move == "q")
                    {
                        WriteLine("Досрочный уход из зоопарка.");
                        break;
                    }
                }

                await Task.Delay(500);
                Clear();
            } 
            while (true);
            
            WriteLine("Передайте парашют выходящему из окна нажатием на любую клавишу.");
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

    }
}