using CarConnect.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CarConnect.Models;

namespace CarConnect.Repository.AdminRepository
{
    internal class AdminRepo : IAdminRepo
    {
        SqlConnection sqlConnection = null;
        SqlCommand sqlCommand = null;

        public AdminRepo()
        {
            sqlConnection = new SqlConnection(DbConnUtil.GetConnString());
            sqlCommand = new SqlCommand();
        }

        public List<Admin> GetAllAdmins()
        {
            sqlCommand.CommandText = "SELECT * FROM Admin";
            sqlCommand.Connection = sqlConnection;

            List<Admin> adminList = new List<Admin>();
            sqlConnection.Open();

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Admin admin = new Admin()
                    {
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        Role = reader["Role"].ToString(),
                    };
                    adminList.Add(admin);
                }
            }

            sqlConnection.Close();
            return adminList;
        }

        public Admin GetAdminById(int adminId)
        {
            sqlCommand.CommandText = "SELECT * FROM Admin WHERE AdminId = @adminId";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@adminId", adminId);
            sqlCommand.Connection = sqlConnection;

            Admin admin = null;
            sqlConnection.Open();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        admin = new Admin
                        {
                            AdminId = (int)reader["AdminId"],
                            FirstName = reader["FirstName"] != DBNull.Value ? (string)reader["FirstName"] : string.Empty,
                            LastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : string.Empty,
                            Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : string.Empty,
                            PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? (string)reader["PhoneNumber"] : string.Empty,
                            Username = reader["Username"] != DBNull.Value ? (string)reader["Username"] : string.Empty,
                            Password = reader["Password"] != DBNull.Value ? (string)reader["Password"] : string.Empty,
                            Role = reader["Role"] != DBNull.Value ? (string)reader["Role"] : string.Empty,
                            JoinDate = reader["JoinDate"] != DBNull.Value ? (DateTime)reader["JoinDate"] : DateTime.MinValue
                        };
                    }
                }
            }
            sqlConnection.Close();
            return admin;
        }

        public Admin GetAdminByUsername(string username)
        {
            sqlCommand.CommandText = "SELECT * FROM Admin WHERE Username = @username";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@username", username);
            sqlCommand.Connection = sqlConnection;

            Admin admin = null;
            sqlConnection.Open();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        admin = new Admin
                        {
                            AdminId = (int)reader["AdminId"],
                            FirstName = reader["FirstName"] != DBNull.Value ? (string)reader["FirstName"] : string.Empty,
                            LastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : string.Empty,
                            Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : string.Empty,
                            PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? (string)reader["PhoneNumber"] : string.Empty,
                            Username = reader["Username"] != DBNull.Value ? (string)reader["Username"] : string.Empty,
                            Password = reader["Password"] != DBNull.Value ? (string)reader["Password"] : string.Empty,
                            Role = reader["Role"] != DBNull.Value ? (string)reader["Role"] : string.Empty,
                            JoinDate = reader["JoinDate"] != DBNull.Value ? (DateTime)reader["JoinDate"] : DateTime.MinValue
                        };
                    }
                }
            }
            sqlConnection.Close();
            return admin;
        }

        public int RegisterAdmin(Admin admin)
        {
            sqlCommand.CommandText = "INSERT INTO Admin (FirstName, LastName, Email, PhoneNumber, Username, Password, Role, JoinDate) VALUES (@firstName, @lastName, @Email, @ph, @username, @password, @role, @date)";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@firstName", admin.FirstName);
            sqlCommand.Parameters.AddWithValue("@lastName", admin.LastName);
            sqlCommand.Parameters.AddWithValue("@Email", admin.Email);
            sqlCommand.Parameters.AddWithValue("@ph", admin.PhoneNumber);
            sqlCommand.Parameters.AddWithValue("@username", admin.Username);
            sqlCommand.Parameters.AddWithValue("@password", admin.Password);
            sqlCommand.Parameters.AddWithValue("@role", admin.Role);
            sqlCommand.Parameters.AddWithValue("@date", admin.JoinDate);
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            int registerStatus = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return registerStatus;
        }

        public int UpdateAdmin(int id, Admin admin)
        {
            sqlCommand.CommandText = "UPDATE Admin SET FirstName = @firstName, LastName = @lastName, Email = @Email, PhoneNumber = @ph, Username = @username, Password = @password, Role = @role, JoinDate = @date WHERE AdminId = @id";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@firstName", admin.FirstName);
            sqlCommand.Parameters.AddWithValue("@lastName", admin.LastName);
            sqlCommand.Parameters.AddWithValue("@Email", admin.Email);
            sqlCommand.Parameters.AddWithValue("@ph", admin.PhoneNumber);
            sqlCommand.Parameters.AddWithValue("@username", admin.Username);
            sqlCommand.Parameters.AddWithValue("@password", admin.Password);
            sqlCommand.Parameters.AddWithValue("@role", admin.Role);
            sqlCommand.Parameters.AddWithValue("@date", admin.JoinDate);
            sqlCommand.Parameters.AddWithValue("@id", id);
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            int updateStatus = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return updateStatus;
        }

        public int DeleteAdmin(int adminId)
        {
            sqlCommand.CommandText = "DELETE FROM Admin WHERE AdminId = @adminId";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@adminId", adminId);
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            int deleteStatus = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return deleteStatus;
        }
    }
}
