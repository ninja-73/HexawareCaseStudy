using CourierManagementSystem.Exceptions;
using CourierManagementSystem.Model;
using CourierManagementSystem.Utility;
using System.Data.SqlClient;

namespace CourierManagementSystem.Repository.User
{
    internal class CourierUserRepo:ICourierUserRepo
    {
        SqlConnection sqlConnection = null;
        SqlCommand sqlCommand = null;

        public CourierUserRepo()
        {
            sqlConnection = new SqlConnection(DbConnUtil.GetConnString());
            sqlCommand = new SqlCommand();
        }

        public int PlaceOrder(Courier courier)
        {
            sqlCommand.CommandText = "insert into Courier (UserID,ServiceID,EmployeeID,ReceiverName,LocationID,Weight,Status,TrackingNumber,OrderedDate,DeliveryDate) values (@userid,@serviceid,@employeeid,@receivername,@locationid,@weight,@status,@trackingnumber,@ordereddate,@deliverydate)";
            sqlCommand.Parameters.AddWithValue("@userid", courier.UserID);
            sqlCommand.Parameters.AddWithValue("@serviceid", courier.ServiceID);
            sqlCommand.Parameters.AddWithValue("@employeeid", courier.EmployeeID);
            sqlCommand.Parameters.AddWithValue("@receivername", courier.ReceiverName);
            sqlCommand.Parameters.AddWithValue("@locationid", courier.LocationID);
            sqlCommand.Parameters.AddWithValue("@weight", courier.Weight);
            sqlCommand.Parameters.AddWithValue("@status", courier.Status);
            sqlCommand.Parameters.AddWithValue("@trackingnumber", courier.TrackingNumber);
            sqlCommand.Parameters.AddWithValue("@ordereddate", courier.OrderedDate);
            sqlCommand.Parameters.AddWithValue("@deliverydate", courier.DeliveryDate);
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            int status = sqlCommand.ExecuteNonQuery();
            return status;
        }

        public string GetOrderStatus(string tnumber)
        {
            sqlCommand.CommandText = "select Status from Courier where TrackingNumber = @tnumber";
            sqlCommand.Parameters.AddWithValue("@tnumber", tnumber);
            sqlCommand.Connection = sqlConnection;
            string status = null;
            sqlConnection.Open();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                // Checking if there are any rows in the result set
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        status = (string)reader["Status"];
                    }
                }
            }
            sqlConnection.Close();
            if (status == null)
            {
                throw new TrackingNumberNotFoundException($"\nNo order found for tracking number: {tnumber}");
            }
            return status;
        }

        public bool CancelOrder(string trackingNumber)
        {
            sqlCommand.CommandText = "update Courier set status=@delivered where TrackingNumber = @tnumber";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@tnumber", trackingNumber);
            sqlCommand.Parameters.AddWithValue("@delivered", "Cancelled");
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            int rowsAffected = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            if (rowsAffected == 0)
            {
                throw new TrackingNumberNotFoundException($"\nCannot cancel. No order found for tracking number: {trackingNumber}");
            }

            return true;
        }      
    }
}
