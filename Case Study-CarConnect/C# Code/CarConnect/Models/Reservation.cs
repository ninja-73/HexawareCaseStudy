using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Models
{
    internal class Reservation
    {
        private int reservationId;
        private int customerId;
        private int vehicleId;
        private DateTime startDate;
        private DateTime endDate;
        private decimal totalCost;
        private string status;

        public Reservation() { }

        // Parameterized constructor
        public Reservation(int customerID, int vehicleID,
                           DateTime startDate, DateTime endDate)
        {
            this.customerId = customerID;
            this.vehicleId = vehicleID;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        //Getters and Setters
        public int ReservationId
        { get { return reservationId; } set { reservationId = value; } }

        public int CustomerId
        { get { return customerId; } set { customerId = value; } }

        public int VehicleId
        { get { return vehicleId; } set { vehicleId = value; } }

        public DateTime StartDate
        { get { return startDate; } set { startDate = value; } }

        public DateTime EndDate
        { get { return endDate; } set { endDate = value; } }

        public decimal TotalCost
        { get { return totalCost; } set { totalCost = value; } }

        public string Status
        { get { return status; } set { status = value; } }

        public override string ToString()
        {
            return $"\nReservationId: {ReservationId},\nCustomerId: {CustomerId},\nVehicleId: {VehicleId},\nStartDate: {StartDate},\nEndDate: {EndDate},\nTotalCost: {TotalCost},\nStatus: {Status}";
        }

    }

    

}
