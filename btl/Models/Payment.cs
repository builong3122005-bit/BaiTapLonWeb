using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace btl.Models
{
    public class Payment
    {
        public string TransactionId { get; set; }
        public int OrderId { get; set; }
        public double Amount { get; set; }
        public string Method { get; set; } // "COD", "VNPAY"
        public string Status { get; set; } // "Success", "Failed"
        public DateTime PaymentDate { get; set; }

        public Payment() { }
    }
}