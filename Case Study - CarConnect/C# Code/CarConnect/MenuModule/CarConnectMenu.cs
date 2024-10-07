using CarConnect.Exceptions;
using CarConnect.Models;
using CarConnect.Service.AdminService;
using CarConnect.Service.CustomerService;
using CarConnect.Service.ReservationService;
using CarConnect.Service.VehicleService;
using static CarConnect.Exceptions.CustomExceptions;

namespace CarConnect.MenuModule
{
    internal class CarConnectMenu
    {
        readonly ICustomerService customerService;
        readonly IVehicleService vehicleService;
        readonly IReservationService reservationService;
        readonly IAdminService adminService;

        public CarConnectMenu()
        {
            try
            {
                customerService = new CustomerService();
                vehicleService = new VehicleService();
                reservationService = new ReservationService();
                adminService = new AdminService();
            }
            catch (DatabaseConnectionException ex)
            {
                // Display the error message to the console
                Console.WriteLine($"Database Connection Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                else
                {
                    Console.WriteLine("No inner exception details available.");
                }
            }
            catch (Exception ex)
            {
                // Catch any other unexpected exceptions
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
        public void Run()
        {
            Console.WriteLine("\t\t\t\t\t\t\tCar Connect\n");
            Console.WriteLine("1. Register Customer\t2.Login\n");
            int ch = int.Parse(Console.ReadLine());
            switch (ch)
            {
                //Register Customer
                case 1:
                    Console.WriteLine("\nEnter First name: ");
                    string fn = Console.ReadLine();
                    Console.WriteLine("Enter last name: ");
                    string ln = Console.ReadLine();
                    Console.WriteLine("Enter email: ");
                    string email = Console.ReadLine();
                    Console.WriteLine("Enter Phone number: ");
                    string ph = Console.ReadLine();
                    Console.WriteLine("Enter Address: ");
                    string address = Console.ReadLine();
                    Console.WriteLine("Enter Username: ");
                    string uname = Console.ReadLine();
                    Console.WriteLine("Enter Password: ");
                    string passw = Console.ReadLine();
                    Customer customer = new Customer()
                    {
                        FirstName = fn,
                        LastName = ln,
                        Email = email,
                        PhoneNumber = ph,
                        Address = address,
                        Username = uname,
                        Password = passw,
                        RegistrationDate = DateTime.Now
                    };
                    customerService.RegisterCustomer(customer);
                    break;
                case 2:
                    int back = 1;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("1.Customer Login\t2.Admin Login");
                        int cusOrAdmin = int.Parse(Console.ReadLine());

                        if (cusOrAdmin == 1)
                        {
                            try
                            {
                                Console.WriteLine("\nEnter username: ");
                                string cusUsername = Console.ReadLine();
                                Console.WriteLine("Enter Password: ");
                                string cusPassword = Console.ReadLine();
                                Customer loginCus = AuthenticateCustomer(cusUsername, cusPassword);

                                //Console Menu for Reservation Management
                                Console.WriteLine("1. GetReservations \n2. CreateReservation\n3. CancelReservation\nEnter Choice: ");
                                int reservationChoice = int.Parse(Console.ReadLine());
                                switch (reservationChoice)
                                {
                                    case 1:
                                        List<Reservation> getReserv = new List<Reservation>();
                                        getReserv = reservationService.GetReservationByCustomerId(loginCus.CustomerId);
                                        foreach (Reservation res in getReserv)
                                        {
                                            Console.WriteLine(res);
                                        }
                                        break;
                                    case 2:
                                        int customerIdInput = loginCus.CustomerId;
                                        vehicleService.GetAvailableVehicles();

                                        try
                                        {
                                            Console.WriteLine("Enter Vehicle ID: ");
                                            int vehicleIdInput = int.Parse(Console.ReadLine());
                                            Console.WriteLine("Enter Start Date (yyyy-mm-dd): ");
                                            DateTime startDate = DateTime.Parse(Console.ReadLine());
                                            Console.WriteLine("Enter End Date (yyyy-mm-dd): ");
                                            DateTime endDate = DateTime.Parse(Console.ReadLine());
                                            Reservation reservation = new Reservation(customerIdInput,vehicleIdInput,startDate,endDate);
                                            reservationService.CreateReservation(reservation);
                                        }
                                        catch (CustomExceptions.VehicleNotFoundException ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;
                                    case 3:
                                        Console.WriteLine("Enter Reservation ID to be Cancelled: ");
                                        int cancelId = int.Parse(Console.ReadLine());
                                        reservationService.CancelReservation(cancelId);
                                        break;
                                    default:
                                        Console.WriteLine("Invalid Input");
                                        break;

                                }
                            }
                            catch (AuthenticationException ex)
                            {
                                Console.WriteLine(ex.Message); 
                                Thread.Sleep(15000); 
                                continue; 
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("Invalid input format. Please enter valid data.");
                                Thread.Sleep(15000); 
                                continue;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                                Thread.Sleep(15000);
                                continue; 
                            }
                        }
                        else if (cusOrAdmin == 2)
                        {
                            //Authentication
                            try
                            {
                                Console.WriteLine("Enter username: ");
                                string admUsername = Console.ReadLine();
                                Console.WriteLine("Enter Password: ");
                                string admPassword = Console.ReadLine();
                                AuthenticateAdmin(admUsername, admPassword);
                                string back1 = "1";
                                do
                                {
                                    Console.Clear();
                                    Console.WriteLine("Which Table do you want to work on: \n1.Customer\n2.Reservation\n3.Vehicle\n4.Admin");
                                    int tableSelect = int.Parse(Console.ReadLine());
                                    if (tableSelect == 1)
                                    {
                                        Console.WriteLine("1.GetCustomerById\n2.GetCustomerByUsername\n3.UpdateCustomer\n4.DeleteCustomer\nEnter Choice: ");
                                        int customerTableCh = int.Parse(Console.ReadLine());
                                        switch (customerTableCh)
                                        {
                                            case 1:
                                                Console.WriteLine("Enter the Customer ID: ");
                                                int id = int.Parse(Console.ReadLine());
                                                Console.WriteLine(customerService.GetCustomerById(id));
                                                break;
                                            case 2:
                                                Console.WriteLine("Enter the Customer Username: ");
                                                string username = Console.ReadLine();
                                                Console.WriteLine(customerService.GetCustomerByUsername(username));
                                                break;
                                            case 3:
                                                Console.WriteLine("Enter the ID to be Updated: ");
                                                int id1 = int.Parse(Console.ReadLine());
                                                Console.WriteLine("Enter First name: ");
                                                string fn1 = Console.ReadLine();
                                                Console.WriteLine("Enter last name: ");
                                                string ln1 = Console.ReadLine();
                                                Console.WriteLine("Enter email: ");
                                                string email1 = Console.ReadLine();
                                                Console.WriteLine("Enter Phone number: ");
                                                string ph1 = Console.ReadLine();
                                                Console.WriteLine("Enter Address: ");
                                                string address1 = Console.ReadLine();
                                                Console.WriteLine("Enter Username: ");
                                                string uname1 = Console.ReadLine();
                                                Console.WriteLine("Enter Password: ");
                                                string passw1 = Console.ReadLine();
                                                Customer customer1 = new Customer()
                                                {
                                                    FirstName = fn1,
                                                    LastName = ln1,
                                                    Email = email1,
                                                    PhoneNumber = ph1,
                                                    Address = address1,
                                                    Username = uname1,
                                                    Password = passw1,
                                                    RegistrationDate = DateTime.Now
                                                };
                                                customerService.UpdateCustomer(id1, customer1);
                                                break;
                                            case 4:
                                                Console.WriteLine("Enter ID to be Deleted: ");
                                                int id2 = int.Parse(Console.ReadLine());
                                                customerService.DeleteCustomer(id2);
                                                break;
                                            default:
                                                Console.WriteLine("Invalid Input");
                                                break;
                                        }
                                    }
                                    else if (tableSelect == 2)
                                    {
                                        //Console Menu for Reservation Management
                                        Console.WriteLine("1. GetReservationById\n2. GetReservationsByCustomerId\n3. UpdateReservation\n4. CancelReservation\nEnter Choice: ");
                                        int resChoice = int.Parse(Console.ReadLine());
                                        switch (resChoice)
                                        {
                                            case 1:
                                                Console.WriteLine("Enter the Reservation ID: ");
                                                int reservationId = int.Parse(Console.ReadLine());
                                                Console.WriteLine(reservationService.GetReservationById(reservationId));
                                                break;

                                            case 2:
                                                Console.WriteLine("Enter Customer ID: ");
                                                int customerId = int.Parse(Console.ReadLine());
                                                List<Reservation> reservations = new List<Reservation>();
                                                reservations = reservationService.GetReservationByCustomerId(customerId);
                                                foreach (Reservation res in reservations)
                                                {
                                                    Console.WriteLine(res);
                                                }
                                                break;
                                            case 3:                                            
                                                Console.WriteLine("Enter Reservation ID to be Updated: ");
                                                int updateId = int.Parse(Console.ReadLine());
                                                Console.WriteLine("Enter New Customer ID: ");
                                                int newCustomerId = int.Parse(Console.ReadLine());
                                                Console.WriteLine("Enter New Vehicle ID: ");
                                                int newVehicleId = int.Parse(Console.ReadLine());
                                                Console.WriteLine("Enter New Start Date (yyyy-mm-dd): ");
                                                DateTime newStartDate = DateTime.Parse(Console.ReadLine());
                                                Console.WriteLine("Enter New End Date (yyyy-mm-dd): ");
                                                DateTime newEndDate = DateTime.Parse(Console.ReadLine());
                                                Console.WriteLine("Enter Status: ");
                                                string status = Console.ReadLine();

                                                Reservation reservation1 = new Reservation()
                                                {
                                                    CustomerId = newCustomerId,
                                                    VehicleId = newVehicleId,
                                                    StartDate = newStartDate,
                                                    EndDate = newEndDate,
                                                    Status = status
                                                };
                                                reservationService.UpdateReservation(updateId, reservation1);
                                                break;
                                            case 4:
                                                Console.WriteLine("Enter Reservation ID to be Cancelled: ");
                                                int cancelId = int.Parse(Console.ReadLine());
                                                reservationService.CancelReservation(cancelId);
                                                break;
                                            default:
                                                Console.WriteLine("Invalid Input");
                                                break;
                                        }
                                    }
                                    else if (tableSelect == 3)
                                    {
                                        //Menu for Vehicle 
                                        Console.WriteLine("1.GetVehicleById\n2.GetAvailableVehicles\n3.AddVehicle\n4.UpdateVehicle\n5.DeleteVehicle\nEnter Choice: ");
                                        int vehicleCh = int.Parse(Console.ReadLine());
                                        switch (vehicleCh)
                                        {
                                            case 1:
                                                Console.WriteLine("Enter the Vehicle ID: ");
                                                int id = int.Parse(Console.ReadLine());
                                                vehicleService.GetVehicleById(id);
                                                break;
                                            case 2:
                                                vehicleService.GetAvailableVehicles();
                                                break;
                                            case 3:
                                                Console.WriteLine("Enter Model: ");
                                                string model = Console.ReadLine();
                                                Console.WriteLine("Enter Make: ");
                                                string make = Console.ReadLine();
                                                Console.WriteLine("Enter year: ");
                                                int year = int.Parse(Console.ReadLine());
                                                Console.WriteLine("Enter color: ");
                                                string color = Console.ReadLine();
                                                Console.WriteLine("Enter Registration Number: ");
                                                string regno = Console.ReadLine();
                                                Console.WriteLine("Enter Availability: ");
                                                int avail = int.Parse(Console.ReadLine());
                                                Console.WriteLine("Enter Daily Rate: ");
                                                decimal drate = decimal.Parse(Console.ReadLine());
                                                Vehicle vehicle = new Vehicle()
                                                {
                                                    Model = model,
                                                    Make = make,
                                                    Year = year,
                                                    Color = color,
                                                    RegistrationNumber = regno,
                                                    Availability = avail,
                                                    DailyRate = drate,
                                                };
                                                vehicleService.AddVehicle(vehicle);
                                                break;
                                            case 4:
                                                Console.WriteLine("Enter the ID to be Updated: ");
                                                int id3 = int.Parse(Console.ReadLine());
                                                Console.WriteLine("Enter Model: ");
                                                string model1 = Console.ReadLine();
                                                Console.WriteLine("Enter Make: ");
                                                string make1 = Console.ReadLine();
                                                Console.WriteLine("Enter year: ");
                                                int year1 = int.Parse(Console.ReadLine());
                                                Console.WriteLine("Enter color: ");
                                                string color1 = Console.ReadLine();
                                                Console.WriteLine("Enter Registration Number: ");
                                                string regno1 = Console.ReadLine();
                                                Console.WriteLine("Enter Availability: ");
                                                int avail1 = int.Parse(Console.ReadLine());
                                                Console.WriteLine("Enter Daily Date: ");
                                                decimal drate1 = decimal.Parse(Console.ReadLine());
                                                Vehicle vehicle1 = new Vehicle()
                                                {
                                                    Model = model1,
                                                    Make = make1,
                                                    Year = year1,
                                                    Color = color1,
                                                    RegistrationNumber = regno1,
                                                    Availability = avail1,
                                                    DailyRate = drate1,
                                                };
                                                vehicleService.UpdateVehicle(id3, vehicle1);
                                                break;
                                            case 5:
                                                Console.WriteLine("Enter ID to be Deleted: ");
                                                int id4 = int.Parse(Console.ReadLine());
                                                vehicleService.RemoveVehicle(id4);
                                                break;
                                            default:
                                                Console.WriteLine("Invalid Input");
                                                break;
                                        }
                                    }
                                    else if (tableSelect == 4)
                                    {
                                        Console.WriteLine("1.GetAdminById\n2.GetAdminByUsername\n3.RegisterAdmin\n4.UpdateAdmin\n5.DeleteAdmin\nEnter Choice: ");
                                        int choice = int.Parse(Console.ReadLine());
                                        switch (choice)
                                        {
                                            case 1:
                                                Console.WriteLine("Enter the Admin ID: ");
                                                int adminid = int.Parse(Console.ReadLine());
                                                adminService.GetAdminById(adminid);
                                                break;
                                            case 2:
                                                Console.WriteLine("Enter the Admin Username: ");
                                                string adminUsername = Console.ReadLine();
                                                Console.WriteLine(adminService.GetAdminByUsername(adminUsername));
                                                break;
                                            case 3:
                                                Console.WriteLine("Enter First Name: ");
                                                string firstName = Console.ReadLine();
                                                Console.WriteLine("Enter Last Name: ");
                                                string lastName = Console.ReadLine();
                                                Console.WriteLine("Enter Email: ");
                                                string adminEmail = Console.ReadLine();
                                                Console.WriteLine("Enter Phone Number: ");
                                                string phoneNumber = Console.ReadLine();
                                                Console.WriteLine("Enter Role: ");
                                                string role = Console.ReadLine();
                                                Console.WriteLine("Enter Username: ");
                                                string username = Console.ReadLine();
                                                Console.WriteLine("Enter Password: ");
                                                string password = Console.ReadLine();

                                                Admin admin = new Admin()
                                                {
                                                    FirstName = firstName,
                                                    LastName = lastName,
                                                    Email = adminEmail,
                                                    PhoneNumber = phoneNumber,
                                                    Role = role,
                                                    Username = username,
                                                    Password = password,
                                                    JoinDate = DateTime.Now
                                                };
                                                adminService.RegisterAdmin(admin);
                                                break;
                                            case 4:
                                                Console.WriteLine("Enter the ID to be Updated: ");
                                                int adminIdToUpdate = int.Parse(Console.ReadLine());
                                                Console.WriteLine("Enter First Name: ");
                                                string updatedFirstName = Console.ReadLine();
                                                Console.WriteLine("Enter Last Name: ");
                                                string updatedLastName = Console.ReadLine();
                                                Console.WriteLine("Enter Email: ");
                                                string updatedEmail = Console.ReadLine();
                                                Console.WriteLine("Enter Phone Number: ");
                                                string updatedPhoneNumber = Console.ReadLine();
                                                Console.WriteLine("Enter Username: ");
                                                string updatedUsername = Console.ReadLine();
                                                Console.WriteLine("Enter Password: ");
                                                string updatedPassword = Console.ReadLine();
                                                Console.WriteLine("Enter Role: ");
                                                string updatedRole = Console.ReadLine();
                                                Console.WriteLine("Enter Join Date:");
                                                DateTime updatedDate = DateTime.Parse(Console.ReadLine());

                                                Admin updatedAdmin = new Admin()
                                                {
                                                    FirstName = updatedFirstName,
                                                    LastName = updatedLastName,
                                                    Email = updatedEmail,
                                                    PhoneNumber = updatedPhoneNumber,
                                                    Username = updatedUsername,
                                                    Password = updatedPassword,
                                                    Role = updatedRole,
                                                    JoinDate = updatedDate
                                                };
                                                adminService.UpdateAdmin(adminIdToUpdate, updatedAdmin);
                                                break;
                                            case 5:
                                                Console.WriteLine("Enter ID to be Deleted: ");
                                                int adminIdToDelete = int.Parse(Console.ReadLine());
                                                adminService.DeleteAdmin(adminIdToDelete);
                                                break;
                                            default:
                                                Console.WriteLine("Invalid Input");
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input");
                                    }
                                        Console.WriteLine("\n1. Go Back\t2. End Session");
                                        back1 = Console.ReadLine(); 
                                } while (back1 == "1");
                            }
                            catch (AuthenticationException ex)
                            {
                                Console.WriteLine(ex.Message); 
                                Thread.Sleep(15000); 
                                continue; 
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("Invalid input format. Please enter valid data.");
                                Thread.Sleep(15000); 
                                continue; 
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                                Thread.Sleep(15000); 
                                continue; 
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Input");
                        }
                        Console.WriteLine("\n1. Go To Login Page\n2.End session");
                        back = int.Parse(Console.ReadLine());
                    } while (back == 1);
                    break;
            }
        }

        public Customer AuthenticateCustomer(string cusUsername, string cusPassword)
        {
            try
            {
                Customer loginCus1 = customerService.GetCustomerByUsername(cusUsername);
                if (loginCus1 == null)
                {
                    throw new AuthenticationException("Username does not exist.");
                }
                if (cusPassword != loginCus1.Password)
                {
                    throw new AuthenticationException("Incorrect password.");
                }
                return loginCus1;
            }
            catch (AuthenticationException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void AuthenticateAdmin(string admUsername, string admPassword)
        {
            Admin loginAdm = adminService.GetAdminByUsername(admUsername);
            if (loginAdm == null)
            {
                throw new AuthenticationException("Username does not exist.");
            }
            if (admPassword != loginAdm.Password)
            {
                throw new AuthenticationException("Incorrect password.");
            }
        }
    }
}


