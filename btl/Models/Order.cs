using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace btl.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CustomerName { get; set; } 
        public double TotalAmount { get; set; }
        public string Status { get; set; } //"Pending", "Processing", "Shipped", "Cancelled"
        public DateTime OrderDate { get; set; }

        public Order() { }
    }
}