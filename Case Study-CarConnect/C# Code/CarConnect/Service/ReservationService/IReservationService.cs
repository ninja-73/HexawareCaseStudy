using CarConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Service.ReservationService
{
    internal interface IReservationService
    {
        void GenerateReport();
        Reservation GetReservationById(int  id);
        List<Reservation> GetReservationByCustomerId(int customerId);
        void CreateReservation(Reservation reservationData);
        void UpdateReservation(int id, Reservation reservationData);
        void CancelReservation(int reservationId);

    }
}
