using CourierManagementSystem.Model;
using CourierManagementSystem.Repository.Admin;
using CourierManagementSystem.Repository.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierManagementSystem.Service.AdminService
{
    internal class CourierAdminService:ICourierAdminService
    {

        readonly ICourierAdminRepo courierAdminRepo;
        public CourierAdminService()
        {
            courierAdminRepo = new CourierAdminRepo();
        }
        public void AddCourierStaff(Employee employee)
        {
            int status = courierAdminRepo.AddCourierStaff(employee);
            if (status > 0)
            {
                Console.WriteLine("Staff Added");
            }
            else
            {
                Console.WriteLine("Addition Failed");
            }
        }

        public void UpdateCourierStatus(string trackingNumber, string newStatus)
        {
            bool status = courierAdminRepo.UpdateCourierStatus(trackingNumber, newStatus);
            if (status == true)
            {
                Console.WriteLine("Status updated");
            }
            else
            {
                Console.WriteLine("Updation Failed!");
            }
        }

        public void GetCourierStaffById(int employeeID)
        {
            Employee emp = courierAdminRepo.GetCourierStaffById(employeeID);
            Console.WriteLine(emp);
        }

        public void UpdateCourierStaff(int id, Employee employee)
        {
            bool status = courierAdminRepo.UpdateCourierStaff(id, employee);
            if (status == true)
            {
                Console.WriteLine("Staff updated");
            }
            else
            {
                Console.WriteLine("Updation Failed!");
            }
        }

        public void RemoveCourierStaff(long employeeID)
        {
            bool status = courierAdminRepo.RemoveCourierStaff(employeeID);
            if (status == true)
            {
                Console.WriteLine("Staff removed");
            }
            else
            {
                Console.WriteLine("Not able to Remove");
            }
        }

        public void GetAssignedOrder(int empid)
        {
            foreach (Courier item in courierAdminRepo.GetAssignedOrder(empid))
            {
                Console.WriteLine(item);
            }

        }

    }

    
}
