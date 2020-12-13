using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChessWeb.SignalR.Client;
using Microsoft.AspNetCore.SignalR;

namespace ChessWeb.SignalR.Server
{
    public class SendboxHub : Hub
    {
        private static readonly Dictionary<string, string> userLookup = new ();

        public async Task SendMessage(string username, string message)
        {
            await Clients.All.SendAsync(MethodNames.Receive, username, message);
            
        }

        public async Task Register(string username)
        {
            var currentId = Context.ConnectionId;
            if (!userLookup.ContainsKey(currentId))
            {
                userLookup.Add(currentId, username);
                await Clients.AllExcept(currentId).SendAsync(
                    MethodNames.Receive,
                    username, $"{username} joined the chat");
            }
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Connected");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            var id = Context.ConnectionId;
            Console.WriteLine($"Disconnected {e?.Message} {id}");
            if (!userLookup.TryGetValue(id, out string username))
                username = "[unknown]";
            userLookup.Remove(id);
            await Clients.AllExcept(id).SendAsync(
                MethodNames.Receive,
                username,
                $"{username} has left the chat");
            await base.OnDisconnectedAsync(e);
        }
    }
}