using System;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalRChat
{
    public class chatHub : Hub
    {
        public void Send()
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.updateKOTlist();
        }
    }
}