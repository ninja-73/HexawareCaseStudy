using CarConnect.Models;
using CarConnect.Utility;
using System.Data.SqlClient;
using static CarConnect.Exceptions.CustomExceptions;

namespace CarConnect.Repository.CustomerRepository
{
    internal class CustomerRepo:ICustomerRepo
    {
        SqlConnection sqlConnection = null;
        SqlCommand sqlCommand = null;

        public CustomerRepo()
        {
                sqlConnection = new SqlConnection(DbConnUtil.GetConnString());
                sqlCommand = new SqlCommand();
        }

        public List<Customer> GetAllCustomers()
        {
            sqlCommand.CommandText = "select * from Customer";
            sqlCommand.Connection = sqlConnection;
            List<Customer> customers = new List<Customer>();
            sqlConnection.Open();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer
                        {
                            CustomerId = (int)reader["CustomerId"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            Email = (string)reader["Email"],
                            PhoneNumber = (string)reader["PhoneNumber"],
                            RegistrationDate = (DateTime)reader["RegistrationDate"]
                        };
                        customers.Add(customer);
                    }
                }
            }
            sqlConnection.Close();
            return customers;
        }


        public Customer GetCustomerById(int Id)
        {
            sqlCommand.CommandText = "select * from Customer where CustomerID = @customerId";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@customerId", Id);
            sqlCommand.Connection = sqlConnection;

            Customer customer = null;
            sqlConnection.Open();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                // Checking if there are any rows in the result set
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        customer = new Customer
                        {
                            CustomerId = (int)reader["CustomerID"],
                            FirstName = reader["FirstName"] != DBNull.Value ? (string)reader["FirstName"] : string.Empty,
                            LastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : string.Empty,
                            Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : string.Empty,
                            PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? (string)reader["PhoneNumber"] : string.Empty,
                            Address = reader["Address"] != DBNull.Value ? (string)reader["Address"] : string.Empty,
                            Password = reader["Password"] != DBNull.Value ? (string)reader["Password"] : string.Empty,
                            RegistrationDate = reader["RegistrationDate"] != DBNull.Value ? (DateTime)reader["RegistrationDate"] : DateTime.MinValue
                        };
                    }
                }
            }
            sqlConnection.Close();
            return customer;
        }

        public Customer GetCustomerByUsername(string uname)
        {
            sqlCommand.CommandText = "select * from Customer where Username = @username1";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@username1", uname);
            sqlCommand.Connection = sqlConnection;

            Customer customer = null;
            sqlConnection.Open();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                // Check if there are any rows in the result set
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        customer = new Customer
                        {
                            CustomerId = (int)reader["CustomerID"],
                            FirstName = reader["FirstName"] != DBNull.Value ? (string)reader["FirstName"] : string.Empty,
                            LastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : string.Empty,
                            Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : string.Empty,
                            PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? (string)reader["PhoneNumber"] : string.Empty,
                            Address = reader["Address"] != DBNull.Value ? (string)reader["Address"] : string.Empty,
                            Username = reader["Username"] != DBNull.Value ? (string)reader["Username"] : string.Empty,
                            Password = reader["Password"] != DBNull.Value ? (string)reader["Password"] : string.Empty,
                            RegistrationDate = reader["RegistrationDate"] != DBNull.Value ? (DateTime)reader["RegistrationDate"] : DateTime.MinValue
                        };
                    }
                }
            }
            sqlConnection.Close();
            return customer;
        }

        public int RegisterCustomer(Customer customer)
        {
            int registerStatus = 0;
            try
            {
                sqlCommand.CommandText = "Insert Into Customer (FirstName,LastName,Email,PhoneNumber,Address,Username,Password,RegistrationDate) values(@firstName,@lastName,@email,@ph,@address,@username,@password,@date)";
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@firstName", customer.FirstName);
                sqlCommand.Parameters.AddWithValue("@lastName", customer.LastName);
                sqlCommand.Parameters.AddWithValue("@Email", customer.Email);
                sqlCommand.Parameters.AddWithValue("@ph", customer.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@address", customer.Address);
                sqlCommand.Parameters.AddWithValue("@username", customer.Username);
                sqlCommand.Parameters.AddWithValue("@password", customer.Password);
                sqlCommand.Parameters.AddWithValue("@date", customer.RegistrationDate);
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                registerStatus = sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601) 
            {
                throw new UniqueConstraintViolationException("EmailID or Username Already Taken", ex);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return registerStatus;
        }

        public int UpdateCustomer(int id, Customer customer)
        {
            sqlCommand.CommandText = "Update Customer set FirstName=@firstName,LastName=@lastName,Email=@email,PhoneNumber=@ph,Address=@address,Username=@username,Password=@password,RegistrationDate=@date where CustomerID=@id";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@id", id);
            sqlCommand.Parameters.AddWithValue("@firstName", customer.FirstName);
            sqlCommand.Parameters.AddWithValue("@lastName", customer.LastName);
            sqlCommand.Parameters.AddWithValue("@Email", customer.Email);
            sqlCommand.Parameters.AddWithValue("@ph", customer.PhoneNumber);
            sqlCommand.Parameters.AddWithValue("@address", customer.Address);
            sqlCommand.Parameters.AddWithValue("@username", customer.Username);
            sqlCommand.Parameters.AddWithValue("@password", customer.Password);
            sqlCommand.Parameters.AddWithValue("@date", customer.RegistrationDate);
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            int registerStatus = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return registerStatus;
        }

        public int DeleteCustomer(int id)
        {
            sqlCommand.CommandText = "delete from Customer where CustomerID = @customerId";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@customerId", id);
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            int registerStatus = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return registerStatus;
        }

    }
}
