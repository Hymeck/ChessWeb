using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChessEngine;
using ChessEngine.Console;
using ChessEngine.Exceptions;
using ChessWeb.SignalR.Shared;
using Microsoft.AspNetCore.SignalR;

namespace ChessWeb.SignalR.Server
{
    public class ChessGameHub : Hub
    {
        private static readonly Dictionary<string, string> _userLookup = new ();
        private static ChessGame _game = new();

        public async Task SendMessage(string username, string message) => 
            await Clients.All.SendAsync(MethodNames.Receive, username, message);

        public async Task ResetBoard()
        {
            _game = new ChessGame();
            var username = _userLookup[Context.ConnectionId];
            await SendMessage(username, "Вернул к истоку доску");
            await Clients.All.SendAsync(MethodNames.Update, _game.ToString());
        }
        
        public async Task Register(string username)
        {
            var currentId = Context.ConnectionId;
            if (!_userLookup.ContainsValue(username))
            {
                _userLookup.Add(currentId, username);
                await Clients.AllExcept(currentId).SendAsync(
                    MethodNames.Receive,
                    username, $"{username} вступил в резвильню");
                Console.WriteLine($"{nameof(ChessGameHub)}. {username} вступил в резвильню");
            }

            else
            {
                await Clients.Client(currentId).SendAsync(
                    MethodNames.Receive,
                    username, $"Некто под этим погонялом уже резвится");
                Console.WriteLine($"{nameof(ChessGameHub)}. Некто под этим погонялом уже резвится");
            }
        }

        public string GetBoard() => _game.ToString();

        public async Task<bool> Move(string move)
        {
            try
            {
                var squares = MoveInputParser.ParseMove(move);
                if (_game.IsCheckmate || _game.IsStalemate)
                {
                    await SendMessage("Состояние игры", "Брань завершена");
                    return true;
                }
                _game = _game.Move(squares);
                var username = _userLookup[Context.ConnectionId];
                await SendMessage(username, $"Выпад {move}");
                await Clients.All.SendAsync(MethodNames.Update, _game.ToString());
                return true;
            }

            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return false;
            }
            
            catch (ChessGameException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"Connected {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            var id = Context.ConnectionId;
            Console.WriteLine($"Disconnected {e?.Message} {id}");
            if (!_userLookup.TryGetValue(id, out var username))
                username = "[Безымень]";
            _userLookup.Remove(id);
            await Clients.AllExcept(id).SendAsync(
                MethodNames.Receive,
                username,
                $"{username} завязал с резвлением");
            Console.WriteLine($"{nameof(ChessGameHub)}. {username} завязал с резвлением");
            await base.OnDisconnectedAsync(e);
        }
    }
}