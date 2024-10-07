using CourierManagementSystem.Exceptions;
using CourierManagementSystem.Model;
using CourierManagementSystem.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierManagementSystem.Repository.Admin
{
    internal class CourierAdminRepo : ICourierAdminRepo
    {
        SqlConnection sqlConnection = null;
        SqlCommand sqlCommand = null;
        public CourierAdminRepo() 
        {
            sqlConnection = new SqlConnection(DbConnUtil.GetConnString());
            sqlCommand = new SqlCommand();
        }

        public int AddCourierStaff(Employee employee)
        {
            sqlCommand.CommandText = "insert into Employee (Name,Email,ContactNumber,Role,Salary) values (@name,@email,@number,@role,@salary)";
            sqlCommand.Parameters.AddWithValue("@name", employee.EmployeeName);
            sqlCommand.Parameters.AddWithValue("@email", employee.Email);
            sqlCommand.Parameters.AddWithValue("@number", employee.ContactNumber);
            sqlCommand.Parameters.AddWithValue("@role", employee.Role);
            sqlCommand.Parameters.AddWithValue("@salary", employee.Salary);
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            int status = sqlCommand.ExecuteNonQuery();
            return status;
        }

        public bool UpdateCourierStatus(string trackingNumber, string newStatus)
        {
            try
            {
                // Set the SQL command to update the courier status
                sqlCommand.CommandText = "UPDATE Courier SET Status = @newStatus WHERE TrackingNumber = @tnumber";
                sqlCommand.Parameters.Clear(); // Clear previous parameters
                sqlCommand.Parameters.AddWithValue("@newStatus", newStatus);
                sqlCommand.Parameters.AddWithValue("@tnumber", trackingNumber);

                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();

                // Execute the query and check if any rows were affected
                int rowsAffected = sqlCommand.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Courier status updated successfully.");
                    return true;
                }
                else
                {
                    throw new TrackingNumberNotFoundException($"Tracking number {trackingNumber} not found.");
                }
            }
            catch (TrackingNumberNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public Employee GetCourierStaffById(int employeeID)
        {
            Employee employee = null;
            try
            {
                sqlCommand.CommandText = "SELECT * FROM Employee WHERE EmployeeID = @employeeID";
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@employeeID", employeeID);

                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            employee = new Employee()
                            {
                                EmployeeID = (int)reader["EmployeeID"],
                                EmployeeName = reader["EmployeeName"].ToString(),
                                Email = reader["Email"].ToString(),
                                ContactNumber = reader["ContactNumber"].ToString(),
                                Role = reader["Role"].ToString(),
                                Salary = (int)reader["Salary"]
                            };
                        }
                    }
                    else
                    {
                        throw new Exception($"Employee with ID {employeeID} not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return employee;
        }

        public bool UpdateCourierStaff(int id, Employee employee)
        {
            try
            {
                sqlCommand.CommandText = "UPDATE Employee SET Name = @name, Email = @Email, ContactNumber = @Contact, Role = @Role, Salary = @Salary WHERE EmployeeID = @employeeID";
                sqlCommand.Parameters.Clear(); 
                sqlCommand.Parameters.AddWithValue("@name", employee.EmployeeName);
                sqlCommand.Parameters.AddWithValue("@Email", employee.Email);
                sqlCommand.Parameters.AddWithValue("@Contact", employee.ContactNumber);
                sqlCommand.Parameters.AddWithValue("@Role", employee.Role);
                sqlCommand.Parameters.AddWithValue("@Salary", employee.Salary);
                sqlCommand.Parameters.AddWithValue("@employeeID", id);

                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();

                int rowsAffected = sqlCommand.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Employee updated successfully.");
                    return true;
                }
                else
                {
                    throw new Exception("Employee update failed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool RemoveCourierStaff(long employeeID)
        {
            try
            {
                sqlCommand.CommandText = "DELETE FROM Employee WHERE EmployeeID = @employeeID";
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@employeeID", employeeID);

                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();

                int rowsAffected = sqlCommand.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Employee removed successfully.");
                    return true;
                }
                else
                {
                    throw new Exception("Employee not found or could not be removed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<Courier> GetAssignedOrder(int courierStaffId)
        {
            sqlCommand.CommandText = "select * from courier where EmployeeID = @id";
            sqlCommand.Parameters.AddWithValue("@id", courierStaffId);
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            List<Courier> couriers = new List<Courier>();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                // Checking if there are any rows in the result set
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Courier courier = new Courier();
                        courier.CourierID = (int)reader["CourierID"];
                        courier.UserID = (int)reader["UserID"];
                        courier.ReceiverName = (string)reader["ReceiverName"];
                        courier.Weight = (decimal)reader["Weight"];
                        courier.ServiceID = (int)reader["ServiceID"];
                        courier.DeliveryDate = (DateTime)reader["DeliveryDate"];
                        courier.Status = (string)reader["Status"];
                        couriers.Add(courier);
                    }
                }
            }
            sqlConnection.Close();
            return couriers;
        }
    }
}
