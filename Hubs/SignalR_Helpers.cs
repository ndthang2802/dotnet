using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;
namespace Application.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message,string group)
        {
            var c = Context.GetHttpContext();
            var user = c.Items["User"];
            await Clients.All.SendAsync("ReceiveMessage",message);
            //await Clients.Group(group).SendAsync("Send", $"{user}: {message}.");
        }

        public async Task SendMessageToGroup(string message,string group)
        {
            var c = Context.GetHttpContext();
            var user = c.Items["User"];
            //await Clients.All.SendAsync("ReceiveMessage",message);
            await Clients.Group(group).SendAsync("Send", $"{user}: {message}.");
        }

        public async Task AddToGroup(string GroupId)
        {
            var c = Context.GetHttpContext();
            Console.WriteLine(c.Items["User"]);

            await Groups.AddToGroupAsync(Context.ConnectionId, GroupId);

            await Clients.Group(GroupId).SendAsync("Send", $"{Context.ConnectionId} has joined the group {GroupId}.");
        }
        // public async Task SendMessage(string GroupId, string message)
        // {
        //     await Clients.Group(GroupId).SendAsync(message);
        // }
    }
} 