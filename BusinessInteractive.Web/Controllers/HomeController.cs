using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessInteractive.Web.Hubs;
using BusinessInteractive.Web.Models;
using SignalR;

namespace BusinessInteractive.Web.Controllers
{
    public class HomeController : Controller
    {
        static List<string> userNames = new List<string>() { "Bob", "Nicola", "Frank", "Kate" };

        private static List<Order> _orders = new List<Order>() {
                new Order() { OrderId = 1, Description = "Description1", DeliveryAddress = "Home", Message="Awesome Message", Value = 12},
                new Order() { OrderId = 2, Description = "Description2", DeliveryAddress = "Home", Message="Awesome Message", Value = 12},
                new Order() { OrderId = 3, Description = "Description3", DeliveryAddress = "Home", Message="Awesome Message", Value = 12},
                new Order() { OrderId = 4, Description = "Description4", DeliveryAddress = "Home", Message="Awesome Message", Value = 12},
                new Order() { OrderId = 5, Description = "Descriptio5", DeliveryAddress = "Home", Message="Awesome Message", Value = 12},
                new Order() { OrderId = 6, Description = "Description6", DeliveryAddress = "Home", Message="Awesome Message", Value = 12},
                new Order() { OrderId = 7, Description = "Descriptio7", DeliveryAddress = "Home", Message="Awesome Message", Value = 12},
                new Order() { OrderId = 8, Description = "Description8", DeliveryAddress = "Home", Message="Awesome Message", Value = 12},
            };

        public List<Order> GetOrders
        { 
            get
            {
                return _orders;
            }
        }

        public ActionResult Index()
        {
            return View(this.GetOrders);
        }


        public ActionResult Detail(int? orderId)
        {
            Order order = this.GetOrders.Where(o => o.OrderId == orderId).Single();

            return View(order);
        }

        [HttpPost]
        public ActionResult Detail(Order order)
        {
            Order updateThisOne = this.GetOrders.Where(o => o.OrderId == order.OrderId).Single();

            updateThisOne.Description = order.Description;
            updateThisOne.DeliveryAddress = order.DeliveryAddress;
            updateThisOne.Message = order.Message;
            updateThisOne.Value = order.Value;

            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients[order.OrderId.ToString()].orderSaved("Bob");

            return View(order);
        }

    }
}
