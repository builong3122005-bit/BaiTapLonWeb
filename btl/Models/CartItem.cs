using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace btl.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public string Size { get; set; }

        public double TotalPrice => Price * Quantity;
    }
}