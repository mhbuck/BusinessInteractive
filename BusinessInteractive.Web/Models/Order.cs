using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessInteractive.Web.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
        public string DeliveryAddress { get; set; }
        public string Message { get; set; }
    }
}