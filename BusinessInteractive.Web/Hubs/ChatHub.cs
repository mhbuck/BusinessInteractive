using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Hubs;

namespace BusinessInteractive.Web.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string orderId, string message)
        {
            //Send messages out to the people who are on the same order
            Clients[orderId].addMessage(message);
        }

        public void StartViewingOrder(string orderId)
        {
            this.Groups.Add(this.Context.ConnectionId, orderId);

            Clients[orderId].newPersonJoined(this.Context.ConnectionId);
        }
    }
}