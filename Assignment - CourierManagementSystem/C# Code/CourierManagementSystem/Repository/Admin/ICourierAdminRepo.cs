using CourierManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierManagementSystem.Repository.Admin
{
    internal interface ICourierAdminRepo
    {
        int AddCourierStaff(Employee employee);

        Employee GetCourierStaffById(int employeeID);

        bool UpdateCourierStaff(int id, Employee employee);

        bool RemoveCourierStaff(long employeeID);

        bool UpdateCourierStatus(string trackingNumber, string newStatus);
        List<Courier> GetAssignedOrder(int courierStaffId);
    }
}
