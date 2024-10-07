using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierManagementSystem.Model
{
    internal class Location
    {
        // Auto-implemented properties
        public long LocationID { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }

        // Default constructor
        public Location() { }

        // Parameterized constructor
        public Location(long locationID, string locationName, string address)
        {
            LocationID = locationID;
            LocationName = locationName;
            Address = address;
        }

        // ToString method
        public override string ToString()
        {
            return $"Location [LocationID={LocationID}, LocationName={LocationName}, Address={Address}]";
        }
    }
}
