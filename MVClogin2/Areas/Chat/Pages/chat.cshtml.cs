using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR.Client;
using MVClogin2.Areas.Identity.Data;
using MVClogin2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVClogin2.Areas.Chat.Pages
{
    public class ChatPageModel : PageModel
    {
    //    public HubConnection hubConnection;
    //    public List<UserMessage> userMessages = new List<UserMessage>();
    //    public string usernameInput;
    //    public string messageInput;
    //    public bool isUserReadOnly = false;
    //    public string username;
    //    UserManager<ApplicationUser> UserManager;
    //    //private readonly NavigationManager navigationManager;

    //    public bool IsConnected => hubConnection.State == HubConnectionState.Connected;

    //    public ChatPageModel(UserManager<ApplicationUser> userManager)
    //    {   
    //        UserManager = userManager;
    //        //username = GetCurrentUser();
    //    }

    //    //protected override async Task OnInitializedAsync()
    //    //{
            
    //    //}
    //    private async Task Send()
    //    {
    //        if (!string.IsNullOrEmpty(usernameInput) && !string.IsNullOrEmpty(messageInput))
    //        {
    //            await hubConnection.SendAsync("SendMessage", usernameInput, messageInput);

    //            isUserReadOnly = true;
    //            messageInput = string.Empty;
    //        }
    //    }

    //    public async ValueTask DisposeAsync()
    //    {
    //        if (hubConnection != null)
    //        {
    //            await hubConnection.DisposeAsync();
    //        }
    //    }
    //    public async Task OnGetAsync()
    //    {
    //        //hubConnection = new HubConnectionBuilder()
    //        //.WithUrl(navigationManager.ToAbsoluteUri("/chathub"))
    //        //.Build();

    //        //hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
    //        //{
    //        //    userMessages.Add(new UserMessage
    //        //    {
    //        //        Username = user,
    //        //        Message = message,
    //        //        IsCurrentUser = user == usernameInput,
    //        //        DateSent = DateTime.Now
    //        //    });

    //        //    //StateHasChanged();
    //        //});

    //        //await hubConnection.StartAsync();
    //    }
    //    //private string GetCurrentUser()
    //    //{
    //    //    return UserManager.GetUserName(User);
    //    //}
    }
}
