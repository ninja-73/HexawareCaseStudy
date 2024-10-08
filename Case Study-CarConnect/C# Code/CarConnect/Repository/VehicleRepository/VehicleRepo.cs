using CarConnect.Models;
using CarConnect.Utility;
using System.Data.SqlClient;

namespace CarConnect.Repository.VehicleRepository
{
    internal class VehicleRepo : IVehicleRepo
    {
        SqlConnection sqlConnection = null;
        SqlCommand sqlCommand = null;
        public VehicleRepo() 
        {
            sqlConnection = new SqlConnection(DbConnUtil.GetConnString());
            sqlCommand = new SqlCommand();
        }

        public Vehicle GetLatestVehicle()
        {
            sqlCommand.CommandText = "SELECT TOP 1 * FROM Vehicle ORDER BY VehicleID DESC"; 
            sqlCommand.Connection = sqlConnection;

            Vehicle vehicle = null;
            sqlConnection.Open();

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        vehicle = new Vehicle
                        {
                            VehicleId = (int)reader["VehicleID"],
                            Model = (string)reader["Model"],
                            Make = (string)reader["Make"],
                            Year = (int)reader["Year"],
                            Color = (string)reader["Color"],
                            RegistrationNumber = (string)reader["RegistrationNumber"],
                            Availability = (int)reader["Availability"],
                            DailyRate = (decimal)reader["DailyRate"]
                        };
                    }
                }
            }

            sqlConnection.Close();
            return vehicle;
        }

        public List<Vehicle> GetAllVehicles()
        {
            sqlCommand.CommandText = "SELECT * FROM Vehicle";
            sqlCommand.Connection = sqlConnection;

            List<Vehicle> allVehicles = new List<Vehicle>();
            sqlConnection.Open();

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Vehicle vehicle = new Vehicle
                    {
                        VehicleId = (int)reader["VehicleID"],
                        Model = (string)reader["Model"],
                        Make = (string)reader["Make"],
                        Year = (int)reader["Year"],
                        Color = (string)reader["Color"],
                        RegistrationNumber = (string)reader["RegistrationNumber"],
                        Availability = (int)reader["Availability"],
                        DailyRate = (decimal)reader["DailyRate"]
                    };
                    allVehicles.Add(vehicle);
                }
            }
            sqlConnection.Close();
            return allVehicles;
        }

        public Vehicle GetVehicleById(int vehicleId)
        {
            sqlCommand.CommandText = "select * from Vehicle where VehicleID = @vehicleid";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@vehicleid", vehicleId);
            sqlCommand.Connection = sqlConnection;
            Vehicle vehicle = null;
            sqlConnection.Open();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        vehicle = new Vehicle()
                        {
                            VehicleId = (int)reader["VehicleID"],
                            Model = (string)reader["Model"],
                            Make = (string)reader["Make"],
                            Year = (int)reader["Year"],
                            Color = (string)reader["Color"],
                            RegistrationNumber = (string)reader["RegistrationNumber"],
                            Availability = (int)reader["Availability"],
                            DailyRate = (decimal)reader["DailyRate"]
                        };
                    }
                }            
                sqlConnection.Close();
                return vehicle;
            }
        }

        public List<Vehicle> GetAvailableVehicles()
        {
            sqlCommand.CommandText = "select * from Vehicle where Availability = @availability";
            sqlCommand.Parameters.Clear(); 
            sqlCommand.Parameters.AddWithValue("@availability", 1);
            sqlCommand.Connection = sqlConnection;

            List<Vehicle> availableVehicles = new List<Vehicle>();
            sqlConnection.Open();

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                // Checking if there are any rows in the result set
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Vehicle vehicle = new Vehicle
                        {
                            VehicleId = (int)reader["VehicleID"],
                            Model = (string)reader["Model"],
                            Make = (string)reader["Make"],
                            Year = (int)reader["Year"],
                            Color = (string)reader["Color"],
                            RegistrationNumber = (string)reader["RegistrationNumber"],
                            Availability = (int)reader["Availability"],
                            DailyRate = (decimal)reader["DailyRate"]
                        };
                        availableVehicles.Add(vehicle); 
                    }
                }
            }
            sqlConnection.Close();
            return availableVehicles;  
        }

        public int AddVehicle(Vehicle vehicle)
        {
            sqlCommand.CommandText = "Insert Into Vehicle (Model, Make, Year, Color, RegistrationNumber, Availability, DailyRate) " +
                "values(@model,@make,@year,@color,@regisnum,@availability,@drate)";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@model", vehicle.Model);
            sqlCommand.Parameters.AddWithValue("@make", vehicle.Make);
            sqlCommand.Parameters.AddWithValue("@year", vehicle.Year);
            sqlCommand.Parameters.AddWithValue("@color", vehicle.Color);
            sqlCommand.Parameters.AddWithValue("@regisnum", vehicle.RegistrationNumber);
            sqlCommand.Parameters.AddWithValue("@availability", vehicle.Availability);
            sqlCommand.Parameters.AddWithValue("@drate", vehicle.DailyRate);
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            int registerStatus = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return registerStatus;
        }

        public int UpdateVehicle(int id, Vehicle vehicle)
        {
            sqlCommand.CommandText = "Update Vehicle set Model=@model,Make=@make,Year=@year,Color=@color,RegistrationNumber=@regisnum,Availability=@availability,DailyRate=@drate" +
                " where VehicleID=@id";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@id", id);
            sqlCommand.Parameters.AddWithValue("@model", vehicle.Model);
            sqlCommand.Parameters.AddWithValue("@make", vehicle.Make);
            sqlCommand.Parameters.AddWithValue("@year", vehicle.Year);
            sqlCommand.Parameters.AddWithValue("@color", vehicle.Color);
            sqlCommand.Parameters.AddWithValue("@regisnum", vehicle.RegistrationNumber);
            sqlCommand.Parameters.AddWithValue("@availability", vehicle.Availability);
            sqlCommand.Parameters.AddWithValue("@drate", vehicle.DailyRate);
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            int registerStatus = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return registerStatus;
        }

        public int RemoveVehicle(int id)
        {
            sqlCommand.CommandText = "delete from Vehicle where VehicleID = @vehicleId";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@vehicleId", id);
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            int registerStatus = sqlCommand.ExecuteNonQuery();
            return registerStatus;
        }
    }
}
