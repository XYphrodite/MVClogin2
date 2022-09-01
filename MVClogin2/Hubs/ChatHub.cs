using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace MVClogin2.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message, string username)
        {
            await Clients.All.SendAsync("SendMessage", message, username);
            //save to database|
            //                |
            //      here      |
            //----------------|
        }
    }
}
