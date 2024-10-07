using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierManagementSystem.Model
{
    internal class CourierCompany
    {
        // Auto-implemented properties
        public string CompanyName { get; set; }
        public List<Courier> CourierDetails { get; set; } = new List<Courier>(); // Initialize with empty list
        public List<Employee> EmployeeDetails { get; set; } = new List<Employee>();
        public List<Location> LocationDetails { get; set; } = new List<Location>();

        // Default constructor
        public CourierCompany() { }

        // Parameterized constructor
        public CourierCompany(string companyName)
        {
            CompanyName = companyName;
        }

        // ToString method
        public override string ToString()
        {
            return $"CourierCompany [CompanyName={CompanyName}]";
        }
    }
}
