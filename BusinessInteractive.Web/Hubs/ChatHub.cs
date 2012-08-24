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
        static List<string> userNames = new List<string>() { "Bob", "Nicola", "Frank", "Kate" };

        public void Send(string orderId, string message)
        {
            // Get a name
            var userName = userNames[orderCount[orderId].IndexOf(Context.ConnectionId)];

            //Send messages out to the people who are on the same order
            Clients[orderId].addMessage(userName, message);
            
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

        public void ControlUpdated(string orderId, string controlId, string controlValue)
        {
            var userName = userNames[orderCount[orderId].IndexOf(Context.ConnectionId)];

            Clients[orderId].controlChanged(controlId, controlValue, this.Context.ConnectionId, userName);
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