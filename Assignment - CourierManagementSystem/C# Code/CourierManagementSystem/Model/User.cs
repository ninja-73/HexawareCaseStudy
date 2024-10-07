using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierManagementSystem.Model
{
    public class User
    {
        // Auto-implemented properties
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }

        // Default constructor
        public User() { }

        // Parameterized constructor
        public User(long userID, string userName, string email, string password, string contactNumber, string address)
        {
            UserID = userID;
            UserName = userName;
            Email = email;
            Password = password;
            ContactNumber = contactNumber;
            Address = address;
        }

        // ToString method
        public override string ToString()
        {
            return $"User [UserID={UserID}, UserName={UserName}, Email={Email}, ContactNumber={ContactNumber}, Address={Address}]";
        }
    }
}

