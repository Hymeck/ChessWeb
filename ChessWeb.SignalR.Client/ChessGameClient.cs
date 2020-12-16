using System;
using System.Text;
using System.Threading.Tasks;
using ChessWeb.SignalR.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChessWeb.SignalR.Client
{
    public sealed class ChessGameClient
    {
        public const string HubUrl = "/chessgamehub";
        private readonly NavigationManager _navigationManager;
        private HubConnection _hubConnection;
        private readonly string _username;
        private bool _started;

        public ChessGameClient(NavigationManager navigationManager, string username)
        {
            _navigationManager = navigationManager;
            _username = username;
        }

        public async Task StartAsync()
        {
            if (!_started)
            {
                // create connection
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(_navigationManager.ToAbsoluteUri(HubUrl))
                    .Build();
                
                // register Events
                _hubConnection.On<string, string>(MethodNames.Receive, HandleReceiveMessage);
                _hubConnection.On<string>(MethodNames.Update, HandleChangedBoard);

                _started = true;
                await _hubConnection.StartAsync();
                await _hubConnection.SendAsync(MethodNames.Register, _username);
            }
        }

        public async Task<bool> Move(string move) => 
            await _hubConnection.InvokeAsync<bool>(MethodNames.Move, move);

        public async Task ResetBoard() =>
            await _hubConnection.InvokeAsync(MethodNames.Reset);

        private void HandleChangedBoard(string fenBoard) => 
            BoardChanged?.Invoke(this, new BoardChangedEventArgs(EditBoard(fenBoard)));

        private void HandleReceiveMessage(string username, string message) => 
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(username, message));
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<BoardChangedEventArgs> BoardChanged;
        
        public async Task StopAsync()
        {
            if (_started)
            {
                await _hubConnection.StopAsync();
                await _hubConnection.DisposeAsync();
                _hubConnection = null;
                _started = false;
            }
        }
        
        public async ValueTask DisposeAsync() => 
            await StopAsync();

        private string[] EditBoard(string board)
        {
            var pieces = board.Split();
            var fenPieces = pieces[0];
            var data = new StringBuilder(fenPieces);
            for (var j = 8; j >= 2; j--)
                data = data.Replace(j.ToString(), (j - 1).ToString() + "1");
            data = data.Replace('1', '.');
            return data.ToString().Split('/');
        }

        public async Task<string[]> GetBoard() => 
            EditBoard(await _hubConnection.InvokeAsync<string>(MethodNames.GetBoard));
    }

    public class MoveReceivedEventArgs : EventArgs
    {
        public string Username { get; }
        public string Move { get; }
        public MoveReceivedEventArgs(string username, string move)
        {
            Username = username;
            Move = move;
        }
    }
    
    public class BoardChangedEventArgs : EventArgs
    {
        public string[] Board { get; }
        public BoardChangedEventArgs(string[] board)
        { 
            Board = board;
        }
    }
    
    
}