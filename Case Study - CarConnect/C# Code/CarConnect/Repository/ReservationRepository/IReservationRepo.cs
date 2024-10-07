using CarConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Repository.ReservationRepository
{
    internal interface IreservationRepo
    {
        Reservation GetReservationById(int reservationId);
        List<Reservation> GetReservationByCustomerId(int customerId);
        int CreateReservation(Reservation reservation);
        int UpdateReservation(int id, Reservation reservation);
        int CancelReservation(int reservationId);

    }
}
