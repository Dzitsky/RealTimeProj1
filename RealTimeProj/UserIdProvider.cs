using Microsoft.AspNetCore.SignalR;

namespace RealTimeProj
{
    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            var httpContext = connection.GetHttpContext();
            var username = httpContext.Request.Query["username"].ToString();
            return username;
        }
    }
}