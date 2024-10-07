using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierManagementSystem.Model
{
    internal class Courier
    {
        static int TrackingNum = 1;
        // Auto-implemented properties
        public long CourierID { get; set; }
        public long UserID { get; set; }
        public string ReceiverName { get; set; }
        public int LocationID { get; set; }
        public int ServiceID { get; set; }
        public int EmployeeID { get; set; }
        public decimal Weight { get; set; }
        public string Status { get; set; }
        public long TrackingNumber { get; set; }
        public DateTime OrderedDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        // Default constructor
        public Courier() 
        { TrackingNum++; }

        // Parameterized constructor
        public Courier(long courierID, int serviceId, int employeeid, string senderName, string senderAddress, string receiverName, int locationId, decimal weight, string status, DateTime orderedDate, DateTime deliveryDate,long trackingNumber, long userID)
        {
            CourierID = courierID;
            LocationID = locationId; 
            ServiceID = serviceId;
            EmployeeID = employeeid;
            ReceiverName = receiverName;
            Weight = weight;
            Status = status;
            TrackingNumber = trackingNumber;
            OrderedDate = orderedDate;
            DeliveryDate = deliveryDate;
            UserID = userID;
        }

        // ToString method
        public override string ToString()
        {
            return $"Courier [CourierID={CourierID}, SenderId={UserID}, ReceiverName={ReceiverName}, TrackingNumber={TrackingNumber}, Status={Status}]";
        }
    }
}
