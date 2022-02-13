using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;
namespace Application.Hubs
{
    [AuthorizeAttribute]
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message,string group)
        {
            var context = Context.GetHttpContext();
            var user = context.Items["User"];
            await Clients.All.SendAsync("ReceiveMessage",message);
            //await Clients.Group(group).SendAsync("Send", $"{user}: {message}.");
        }

        public async Task SendMessageToGroup(string message,string group)
        {
            var context = Context.GetHttpContext();
            var user = context.Items["User"];
            await Clients.Group(group).SendAsync("Send", $"{user}:{message}.");
        }

        public async Task AddToGroup(string GroupId)
        {
            

            await Groups.AddToGroupAsync(Context.ConnectionId, GroupId);

            await Clients.Group(GroupId).SendAsync("AddS", $"{Context.ConnectionId} has joined the group {GroupId}.");
        }
        // public async Task SendMessage(string GroupId, string message)
        // {
        //     await Clients.Group(GroupId).SendAsync(message);
        // }
    }
} 