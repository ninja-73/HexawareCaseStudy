using CarConnect.Models;
using CarConnect.Repository.CustomerRepository;
using CarConnect.Repository.ReservationRepository;
using CarConnect.Repository.VehicleRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CarConnect.Exceptions.CustomExceptions;

namespace CarConnect.Service.ReservationService
{
    internal class ReservationService:IReservationService
    {
        readonly IreservationRepo reservationRepo;
        public ReservationService()
        {
            reservationRepo = new ReservationRepo();
        }

        public void GenerateReport()
        {
            List<Reservation> res = reservationRepo.GenerateReport();
            foreach (Reservation r in res)
            {
                Console.WriteLine(r);
            }
        }

        public Reservation GetReservationById(int id)
        {
            Reservation reservation = new Reservation();
            reservation = reservationRepo.GetReservationById(id);
            if (reservation == null)
            {
                throw new ReservationException("Reservation data is invalid.");
            }
            return reservation;
        }

        public List<Reservation> GetReservationByCustomerId(int customerId)
        {
            List<Reservation> reservations = new List<Reservation>();
            reservations = reservationRepo.GetReservationByCustomerId(customerId);
            if (reservations == null)
            {
                throw new ReservationException("No Reservations");
            }
            return reservations; 
        }

        public void CreateReservation(Reservation reservation)
        {
            int addStatus = reservationRepo.CreateReservation(reservation);
            if (addStatus > 0)
            {
                Console.WriteLine("Reservation added Successfully");
            }
            else
            {
                Console.WriteLine("Addition Failed");
            }
        }

        public void UpdateReservation(int id, Reservation reservation)
        {
            int Status = reservationRepo.UpdateReservation(id, reservation);
            if (Status > 0)
            {
                Console.WriteLine("Reservation updated Successfully");
            }
            else
            {
                Console.WriteLine("Updation Failed");
            }
        }

        public void CancelReservation(int reservationId)
        {
            int Status = reservationRepo.CancelReservation(reservationId);
            if (Status > 0)
            {
                Console.WriteLine("Reservation canceled Successfully");
            }
            else
            {
                Console.WriteLine("Cancelation Failed");
            }
        }        
    }
}
