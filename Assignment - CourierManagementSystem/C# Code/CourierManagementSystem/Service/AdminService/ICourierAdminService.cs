using CourierManagementSystem.Model;

namespace CourierManagementSystem.Service.AdminService
{
    internal interface ICourierAdminService
    {
        void AddCourierStaff(Employee employee);
        void UpdateCourierStaff(int id, Employee employee);

        void UpdateCourierStatus(string trackingNumber, string newStatus);
        void GetCourierStaffById(int employeeID);
        void RemoveCourierStaff(long employeeID);

        void GetAssignedOrder(int empid);
    }
}
