using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace RealTimeProj.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string to, string from, string message)
        {
            message += " (хаб)";
            await Clients.Users(to, from).SendAsync("message", new {
                from, 
                message
            });
        }
    }
}