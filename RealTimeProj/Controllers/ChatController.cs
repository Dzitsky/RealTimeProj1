using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RealTimeProj.Hubs;

namespace RealTimeProj.Controllers
{
    [ApiController]
    [Route("ChatApi")]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext; 
        
        public ChatController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task Get(string to, string from, string message)
        {
            message += " (апи)";
            await _hubContext.Clients.Users(to, from).SendAsync("message", new {
                from, 
                message
            });
        }
    }
}