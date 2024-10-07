using System;
using System.Collections.Generic;
using CourierManagementSystem.Model;
using CourierManagementSystem.Service.UserService;
using CourierManagementSystem.Service.AdminService;

namespace CourierManagementSystem.MenuModule
{
    public class MainMenu
    {
        private readonly ICourierUserService userService;
        private readonly ICourierAdminService adminService;

        public MainMenu()
        {
            userService = new CourierUserService();
            adminService = new CourierAdminService();
        }

        public void Run()
        {
            Console.WriteLine("\t\t\t\t\t\t\tCourier Management System\n");
            Login();
        }

        private void Login()
        {
            Console.Clear();
            Console.WriteLine("1. Customer Login\t2. Admin Login");
            int cusOrAdmin = int.Parse(Console.ReadLine());

            if (cusOrAdmin == 1)
            {
                CustomerLogin();
            }
            else if (cusOrAdmin == 2)
            {
                AdminLogin();
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
            }
        }

        private void CustomerLogin()
        {
            Console.WriteLine("\nEnter Username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Enter Password: ");
            string password = Console.ReadLine();

            try
            {
                Console.WriteLine("Login successful! Choose an option:");
                Console.WriteLine("1. Place Order\n2. Get Order Status\n3. Cancel Order");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        PlaceOrder();
                        break;
                    case 2:
                        GetOrderStatus();
                        break;
                    case 3:
                        CancelOrder();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AdminLogin()
        {
            Console.WriteLine("Enter Admin Username: ");
            string adminUsername = Console.ReadLine();
            Console.WriteLine("Enter Admin Password: ");
            string adminPassword = Console.ReadLine();

            try
            {
                Console.WriteLine("Login successful! Choose an option:");
                Console.WriteLine("1. Add Courier Staff\n2. Update Courier Staff\n3. Get Courier Staff By ID\n4. Remove Courier Staff\n5. Update Courier Status\n6. Get Assigned Orders");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddCourierStaff();
                        break;
                    case 2:
                        UpdateCourierStaff();
                        break;
                    case 3:
                        GetCourierStaffById();
                        break;
                    case 4:
                        RemoveCourierStaff();
                        break;
                    case 5:
                        UpdateCourierStatus();
                        break;
                    case 6:
                        GetAssignedOrders();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void PlaceOrder()
        {
            Console.WriteLine("Enter Courier details to place the order...");

            Courier courier = new Courier();

            // Gather details from the user
            Console.Write("Enter User ID: ");
            courier.UserID = long.Parse(Console.ReadLine());

            Console.Write("Enter Receiver Name: ");
            courier.ReceiverName = Console.ReadLine();

            Console.Write("Enter Location ID: ");
            courier.LocationID = int.Parse(Console.ReadLine());

            Console.Write("Enter Service ID: ");
            courier.ServiceID = int.Parse(Console.ReadLine());

            Console.Write("Enter Employee ID: ");
            courier.EmployeeID = int.Parse(Console.ReadLine());

            Console.Write("Enter Weight: ");
            courier.Weight = decimal.Parse(Console.ReadLine());

            courier.Status = "Pending";

            Console.Write("Enter Tracking Number: ");
            courier.TrackingNumber = long.Parse(Console.ReadLine());

            courier.OrderedDate = DateTime.Now; 

            courier.DeliveryDate = DateTime.Now.AddDays(3);

            userService.PlaceOrder(courier);
            Console.WriteLine("Order placed successfully!");
        }

        private void GetOrderStatus()
        {
            Console.WriteLine("Enter Tracking Number: ");
            string trackingNumber = Console.ReadLine();
            userService.GetOrderStatus(trackingNumber);
        }

        private void CancelOrder()
        {
            Console.WriteLine("Enter Tracking Number to cancel the order: ");
            string trackingNumber = Console.ReadLine();
            userService.CancelOrder(trackingNumber);
        }

        private void AddCourierStaff()
        {
            Console.WriteLine("Enter Courier Staff details...");

            Employee employee = new Employee();

            // Prompt for employee details
            Console.WriteLine("Enter Employee Name: ");
            employee.EmployeeName = Console.ReadLine();

            Console.WriteLine("Enter Email: ");
            employee.Email = Console.ReadLine();

            Console.WriteLine("Enter Contact Number: ");
            employee.ContactNumber = Console.ReadLine();

            Console.WriteLine("Enter Role: ");
            employee.Role = Console.ReadLine();

            Console.WriteLine("Enter Salary: ");
            employee.Salary = double.Parse(Console.ReadLine());

            adminService.AddCourierStaff(employee);
        }

        private void UpdateCourierStaff()
        {
            Console.WriteLine("Enter Employee ID to update: ");
            int staffId = int.Parse(Console.ReadLine());
            Employee existingEmployee = new Employee();
            Console.WriteLine("Enter new Employee Name: ");
            existingEmployee.EmployeeName = Console.ReadLine();

            Console.WriteLine("Enter new Email: ");
            existingEmployee.Email = Console.ReadLine();

            Console.WriteLine("Enter new Contact Number: ");
            existingEmployee.ContactNumber = Console.ReadLine();

            Console.WriteLine("Enter new Role: ");
            existingEmployee.Role = Console.ReadLine();

            Console.WriteLine("Enter new Salary: ");
            string newSalaryInput = Console.ReadLine();

            adminService.UpdateCourierStaff(staffId, existingEmployee);
        }

        private void GetCourierStaffById()
        {
            Console.WriteLine("Enter Employee ID to retrieve: ");
            int employeeID = int.Parse(Console.ReadLine());
            adminService.GetCourierStaffById(employeeID);
        }

        private void RemoveCourierStaff()
        {
            Console.WriteLine("Enter Employee ID to remove: ");
            long employeeID = long.Parse(Console.ReadLine());
            adminService.RemoveCourierStaff(employeeID);
        }

        private void UpdateCourierStatus()
        {
            Console.WriteLine("Enter Tracking Number to update status: ");
            string trackingNumber = Console.ReadLine();
            Console.WriteLine("Enter new status: ");
            string newStatus = Console.ReadLine();
            adminService.UpdateCourierStatus(trackingNumber, newStatus);
            Console.WriteLine("Courier status updated successfully!");
        }

        private void GetAssignedOrders()
        {
            Console.WriteLine("Enter Courier Staff ID to get assigned orders: ");
            int courierStaffId = int.Parse(Console.ReadLine());
            adminService.GetAssignedOrder(courierStaffId);

        }
    }
}
