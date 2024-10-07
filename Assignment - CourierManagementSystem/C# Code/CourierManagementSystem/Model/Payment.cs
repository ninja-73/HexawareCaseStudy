using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierManagementSystem.Model
{
    internal class Payment
    {
        // Auto-implemented properties
        public long PaymentID { get; set; }
        public long CourierID { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        // Default constructor
        public Payment() { }

        // Parameterized constructor
        public Payment(long paymentID, long courierID, double amount, DateTime paymentDate)
        {
            PaymentID = paymentID;
            CourierID = courierID;
            Amount = amount;
            PaymentDate = paymentDate;
        }

        // ToString method
        public override string ToString()
        {
            return $"Payment [PaymentID={PaymentID}, CourierID={CourierID}, Amount={Amount}, PaymentDate={PaymentDate}]";
        }
    }
}
