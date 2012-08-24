using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SignalR.Hubs;

namespace BusinessInteractive.Web.Hubs
{
    public class ChatHub : Hub, IDisconnect
    {
        static Dictionary<string, List<string>> orderCount = new Dictionary<string, List<string>>();

        public void Send(string orderId, string message)
        {
            //Send messages out to the people who are on the same order
            Clients[orderId].addMessage(message);
            
        }

        public void StartViewingOrder(string orderId)
        {
            if (!orderCount.ContainsKey(orderId))
            {
                orderCount.Add(orderId, new List<string>());
            }

            orderCount[orderId].Add(Context.ConnectionId);

            
            this.Groups.Add(this.Context.ConnectionId, orderId);

            Clients[orderId].newPersonJoinedOrLeft(orderCount[orderId].Count);
        }

        public Task Disconnect()
        {
            foreach (KeyValuePair<string, List<string>> item in orderCount)
            {
                if (item.Value.Contains(this.Context.ConnectionId))
                {
                    item.Value.Remove(this.Context.ConnectionId);
                }

                Clients[item.Key].newPersonJoinedOrLeft(orderCount[item.Key].Count);

            }

            return null;
        }
    }
}