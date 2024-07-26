﻿using Microsoft.AspNetCore.SignalR;

namespace UdemySignalR.Web.Hubs
{
    public class MyHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);

        }



    }
}
