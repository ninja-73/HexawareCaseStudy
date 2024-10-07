using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierManagementSystem.Model
{
    internal class Employee
    {
        // Auto-implemented properties
        public long EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Role { get; set; }
        public double Salary { get; set; }

        // Default constructor
        public Employee() { }

        // Parameterized constructor
        public Employee(long employeeID, string employeeName, string email, string contactNumber, string role, double salary)
        {
            EmployeeID = employeeID;
            EmployeeName = employeeName;
            Email = email;
            ContactNumber = contactNumber;
            Role = role;
            Salary = salary;
        }

        // ToString method
        public override string ToString()
        {
            return $"Employee [EmployeeID={EmployeeID}, EmployeeName={EmployeeName}, Role={Role}]";
        }
    }
}
