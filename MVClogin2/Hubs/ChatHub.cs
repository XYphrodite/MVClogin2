using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MVClogin2.Models;
using MVClogin2.Sql;
using System;
using System.Threading.Tasks;

namespace MVClogin2.Hubs
{
    public class ChatHub : Hub
    {
        private ISqlWorker sqlWorker;
        public ChatHub(ISqlWorker sqlWorker)
        {
            this.sqlWorker = sqlWorker;
        }
        public async Task SendMessage(string message, string username)
        {
            await Clients.All.SendAsync("SendMessage", message, username);
            sqlWorker.SaveMessageToDb(new UserMessage
            {
                Message = message,
                Username=username,
                userId=sqlWorker.getIdByUsername(username),
                DateSent=DateTime.Now
            });
        }
    }
}
