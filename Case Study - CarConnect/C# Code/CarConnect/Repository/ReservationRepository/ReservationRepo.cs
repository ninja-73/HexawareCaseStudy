using CarConnect.Exceptions;
using CarConnect.Models;
using CarConnect.Utility;
using System.Data.SqlClient;

namespace CarConnect.Repository.ReservationRepository
{
    internal class ReservationRepo:IreservationRepo
    {
        SqlConnection sqlConnection = null;
        SqlCommand sqlCommand = null;
        public ReservationRepo()
        {
            sqlConnection = new SqlConnection(DbConnUtil.GetConnString());
            sqlCommand = new SqlCommand();
        }

        public Reservation GetReservationById(int reserveid)
        {
            Reservation reservation = null;
            sqlCommand.CommandText = "select * from Reservation where ReservationId = @reservationId";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@reservationId", reserveid);
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        reservation = new Reservation()
                        {
                            ReservationId = (int)reader["ReservationId"],
                            CustomerId = (int)reader["CustomerId"],
                            VehicleId = (int)reader["VehicleId"],
                            StartDate = (DateTime)reader["StartDate"],
                            EndDate = (DateTime)reader["EndDate"],
                            TotalCost = (decimal)reader["TotalCost"],
                            Status = (string)reader["Status"]
                        };
                    }
                }
            }

            sqlConnection.Close();
            return reservation;
        }

        public List<Reservation> GetReservationByCustomerId(int customerid)
        {
            List<Reservation> reservations = new List<Reservation>();
            sqlCommand.CommandText = "select * from Reservation where CustomerID = @customerId";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@customerId", customerid);
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Reservation reservation = new Reservation()
                        {
                            ReservationId = (int)reader["ReservationId"],
                            CustomerId = (int)reader["CustomerId"],
                            VehicleId = (int)reader["VehicleId"],
                            StartDate = (DateTime)reader["StartDate"],
                            EndDate = (DateTime)reader["EndDate"],
                            TotalCost = (decimal)reader["TotalCost"],
                            Status = (string)reader["Status"]
                        };
                        reservations.Add(reservation);
                    }
                }
            }
            sqlConnection.Close();
            return reservations;
        }

        public int CreateReservation(Reservation reservation)
        {
            Vehicle vehicle = new Vehicle();
            vehicle.VehicleId = reservation.VehicleId;

            sqlCommand.CommandText = "SELECT DailyRate FROM Vehicle WHERE VehicleId = @vehicleId";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@vehicleId", reservation.VehicleId);
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (!reader.Read())
                {
                    // If no record is found for the vehicle, throw VehicleNotFoundException
                    throw new CustomExceptions.VehicleNotFoundException($"Vehicle with ID {reservation.VehicleId} not found.");
                }
                vehicle.DailyRate = (decimal)reader["DailyRate"]; // Retrieve the daily rate
            }

            sqlCommand.CommandText = "insert into Reservation (CustomerId, VehicleId, StartDate, EndDate, TotalCost, Status) VALUES (@customerId, @vehicleId1, @startDate, @endDate, @totalCost, @status)";
            sqlCommand.Parameters.AddWithValue("@customerId", reservation.CustomerId);
            sqlCommand.Parameters.AddWithValue("@vehicleId1", vehicle.VehicleId);

            sqlCommand.Parameters.AddWithValue("@startDate", reservation.StartDate); 
            sqlCommand.Parameters.AddWithValue("@endDate", reservation.EndDate);
            int diff = (reservation.EndDate.Subtract(reservation.StartDate)).Days;
            decimal TotalCost = (vehicle.DailyRate * diff);
            sqlCommand.Parameters.AddWithValue("@totalCost", TotalCost);
            sqlCommand.Parameters.AddWithValue("@status", "Pending");
            sqlCommand.Connection = sqlConnection;

            int registerStatus = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            Console.WriteLine($"\nThe Total cost is: {TotalCost}\n");
            return registerStatus; 
        }

        public int UpdateReservation(int id, Reservation reservation)
        {
            sqlCommand.CommandText = @"UPDATE Reservation 
            SET CustomerId = @customerId,
                VehicleId = @vehicleId,
                StartDate = @startDate,
                EndDate = @endDate,
                TotalCost = @totalCost,
                Status = @status
            WHERE ReservationId = @id";
            sqlCommand.Parameters.Clear();

            sqlCommand.Parameters.AddWithValue("@customerId", reservation.CustomerId);
            sqlCommand.Parameters.AddWithValue("@vehicleId", reservation.VehicleId);
            sqlCommand.Parameters.AddWithValue("@startDate", reservation.StartDate);
            sqlCommand.Parameters.AddWithValue("@endDate", reservation.EndDate);
            sqlCommand.Parameters.AddWithValue("@totalCost", reservation.TotalCost);
            sqlCommand.Parameters.AddWithValue("@status", reservation.Status);
            sqlCommand.Parameters.AddWithValue("@id", id); 

            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();

            int registerStatus = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return registerStatus;
        }

        public int CancelReservation(int reservationId)
        {
            sqlCommand.CommandText = "UPDATE Reservation SET Status = @status WHERE ReservationId = @id";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@status", "Cancelled"); // Set status to "Cancelled"
            sqlCommand.Parameters.AddWithValue("@id", reservationId); 

            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();

            int cancelStatus = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close ();
            return cancelStatus; 
        }


    }
}
