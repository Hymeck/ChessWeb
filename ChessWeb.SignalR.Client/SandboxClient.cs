using System;
using System.Threading.Tasks;
using ChessWeb.SignalR.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChessWeb.SignalR.Client
{
    public class SandboxClient : IAsyncDisposable
    {
        public const string HubUrl = "/sandboxhub";
        private readonly NavigationManager _navigationManager;
        private HubConnection _hubConnection;
        private readonly string _username;
        private bool _started = false;

        public SandboxClient(string username, NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            _username = username;
        }

        public async Task StartAsync()
        {
            if (!_started)
            {
                _hubConnection =
                    new HubConnectionBuilder()
                        .WithUrl(_navigationManager.ToAbsoluteUri(HubUrl))
                        .Build();
                Console.WriteLine($"{nameof(SandboxClient)}: calling {nameof(StartAsync)}");

                _hubConnection.On<string, string>(MethodNames.Receive, HandleReceiveMessage);

                await _hubConnection.StartAsync();
                Console.WriteLine($"{nameof(SandboxClient)}: {nameof(StartAsync)} returned");
                _started = true;

                await _hubConnection.SendAsync(MethodNames.Register, _username);
            }
        }

        private void HandleReceiveMessage(string username, string message)
        {
            // MessageReceived?.Invoke(this, new MessageReceivedEventArgs(username, message));
        }

        // public event MessageReceivedEventHandler MessageReceived;
        //
        // public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);

        public async Task SendAsync(string message)
        {
            if (!_started)
            {
                throw new InvalidOperationException("Client not started");
            }

            await _hubConnection.SendAsync(MethodNames.Send, _username, message);
        }

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
        
        public async ValueTask DisposeAsync()
        {
            Console.WriteLine($"{nameof(SandboxClient)}: {nameof(DisposeAsync)}");
            await StopAsync();
        }
    }

    public class MessageReceivedEventArgs : EventArgs
    {
        public string Username { get; set; }
        public string Message { get; set; }
        public MessageReceivedEventArgs(string username, string message)
        {
            Username = username;
            Message = message;
        }
    }
}