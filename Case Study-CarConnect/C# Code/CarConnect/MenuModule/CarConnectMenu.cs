using CarConnect.Exceptions;
using CarConnect.Models;
using CarConnect.Service.AdminService;
using CarConnect.Service.CustomerService;
using CarConnect.Service.ReservationService;
using CarConnect.Service.VehicleService;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
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
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
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

                    bool isPasswordValid = false;
                    string passw = "";
                    while (!isPasswordValid)
                    {
                        Console.WriteLine("Enter Password (At least 4 characters, 1 special character, and 1 number): ");
                        passw = Console.ReadLine();

                        if (ValidatePassword(passw))
                        {
                            isPasswordValid = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid password. Please try again.");
                        }
                    }
                    bool ValidatePassword(string password)
                    {
                        if (password.Length < 4)
                            return false;

                        bool hasSpecialChar = Regex.IsMatch(password, @"[!@#$%^&*(),.?""':{}|<>]");
                        bool hasNumber = Regex.IsMatch(password, @"\d");

                        return hasSpecialChar && hasNumber;
                    }
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
                                            if(startDate < endDate)
                                            {
                                                Reservation reservation = new Reservation(customerIdInput, vehicleIdInput, startDate, endDate);
                                                reservationService.CreateReservation(reservation);
                                            }
                                            else
                                            {
                                                Console.WriteLine("End date must be after the start date!");
                                            }
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
                                Thread.Sleep(8000); 
                                continue; 
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("Invalid input format. Please enter valid data.");
                                Thread.Sleep(8000); 
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
                                    Console.WriteLine("Which Table do you want to work on: \n1.Customer\n2.Reservation\n3.Vehicle\n4.Admin\n");
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
                                        Console.WriteLine("\n1. GetReservationById\n2. GetReservationsByCustomerId\n3. UpdateReservation\n4. CancelReservation\n5.GenerateReport\nEnter Choice: ");
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

                                                Console.WriteLine("Enter New Customer ID (leave blank to keep existing): ");
                                                string customerIdInput = Console.ReadLine();
                                                Console.WriteLine("Enter New Vehicle ID (leave blank to keep existing): ");
                                                string vehicleIdInput = Console.ReadLine();
                                                Console.WriteLine("Enter New Start Date (yyyy-mm-dd, leave blank to keep existing): ");
                                                string startDateInput = Console.ReadLine();
                                                Console.WriteLine("Enter New End Date (yyyy-mm-dd, leave blank to keep existing): ");
                                                string endDateInput = Console.ReadLine();
                                                Console.WriteLine("Enter Status (leave blank to keep existing): ");
                                                string statusInput = Console.ReadLine();

                                                Reservation existingReservation = reservationService.GetReservationById(updateId);
                                                Reservation reservation1 = new Reservation()
                                                {
                                                    CustomerId = !string.IsNullOrWhiteSpace(customerIdInput) ? int.Parse(customerIdInput) : existingReservation.CustomerId,
                                                    VehicleId = !string.IsNullOrWhiteSpace(vehicleIdInput) ? int.Parse(vehicleIdInput) : existingReservation.VehicleId,
                                                    StartDate = !string.IsNullOrWhiteSpace(startDateInput) ? DateTime.Parse(startDateInput) : existingReservation.StartDate,
                                                    EndDate = !string.IsNullOrWhiteSpace(endDateInput) ? DateTime.Parse(endDateInput) : existingReservation.EndDate,
                                                    Status = !string.IsNullOrWhiteSpace(statusInput) ? statusInput : existingReservation.Status
                                                };

                                                reservationService.UpdateReservation(updateId, reservation1);
                                                break;
                                            case 4:
                                                Console.WriteLine("Enter Reservation ID to be Cancelled: ");
                                                int cancelId = int.Parse(Console.ReadLine());
                                                reservationService.CancelReservation(cancelId);
                                                break;
                                            case 5:
                                                reservationService.GenerateReport();
                                                break;
                                            default:
                                                Console.WriteLine("Invalid Input");
                                                break;
                                        }
                                    }
                                    else if (tableSelect == 3)
                                    {
                                        //Menu for Vehicle 
                                        Console.WriteLine("\n1.GetVehicleById\n2.GetAvailableVehicles\n3.AddVehicle\n4.UpdateVehicle\n5.DeleteVehicle\nEnter Choice: ");
                                        int vehicleCh = int.Parse(Console.ReadLine());
                                        switch (vehicleCh)
                                        {
                                            case 1:
                                                Console.WriteLine("Enter the Vehicle ID: ");
                                                int id = int.Parse(Console.ReadLine());
                                                Console.WriteLine(vehicleService.GetVehicleById(id));

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
                                                Vehicle vehicle2 = new Vehicle()
                                                {
                                                    Model = model,
                                                    Make = make,
                                                    Year = year,
                                                    Color = color,
                                                    RegistrationNumber = regno,
                                                    Availability = avail,
                                                    DailyRate = drate,
                                                };
                                                vehicleService.AddVehicle(vehicle2);
                                                break;
                                            case 4:
                                                Console.WriteLine("Enter the ID to be Updated: ");
                                                int id3 = int.Parse(Console.ReadLine());

                                                // Fetch existing vehicle details
                                                Vehicle existingVehicle = vehicleService.GetVehicleById(id3);

                                                Console.WriteLine("Enter Model (leave blank to keep existing): ");
                                                string model1 = Console.ReadLine();
                                                Console.WriteLine("Enter Make (leave blank to keep existing): ");
                                                string make1 = Console.ReadLine();
                                                Console.WriteLine("Enter Year (leave blank to keep existing): ");
                                                string yearInput = Console.ReadLine();
                                                Console.WriteLine("Enter Color (leave blank to keep existing): ");
                                                string color1 = Console.ReadLine();
                                                Console.WriteLine("Enter Registration Number (leave blank to keep existing): ");
                                                string regno1 = Console.ReadLine();
                                                Console.WriteLine("Enter Availability (leave blank to keep existing): ");
                                                string availInput = Console.ReadLine();
                                                Console.WriteLine("Enter Daily Rate (leave blank to keep existing): ");
                                                string drateInput = Console.ReadLine();

                                                // Create updated vehicle object with conditional values
                                                Vehicle vehicle1 = new Vehicle()
                                                {
                                                    Model = !string.IsNullOrWhiteSpace(model1) ? model1 : existingVehicle.Model,
                                                    Make = !string.IsNullOrWhiteSpace(make1) ? make1 : existingVehicle.Make,
                                                    Year = !string.IsNullOrWhiteSpace(yearInput) ? int.Parse(yearInput) : existingVehicle.Year,
                                                    Color = !string.IsNullOrWhiteSpace(color1) ? color1 : existingVehicle.Color,
                                                    RegistrationNumber = !string.IsNullOrWhiteSpace(regno1) ? regno1 : existingVehicle.RegistrationNumber,
                                                    Availability = !string.IsNullOrWhiteSpace(availInput) ? int.Parse(availInput) : existingVehicle.Availability,
                                                    DailyRate = !string.IsNullOrWhiteSpace(drateInput) ? decimal.Parse(drateInput) : existingVehicle.DailyRate,
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
                                        Console.WriteLine("\n1.GetAdminById\n2.GetAdminByUsername\n3.RegisterAdmin\n4.UpdateAdmin\n5.DeleteAdmin\nEnter Choice: ");
                                        int choice = int.Parse(Console.ReadLine());
                                        switch (choice)
                                        {
                                            case 1:
                                                Console.WriteLine("Enter the Admin ID: ");
                                                int adminid = int.Parse(Console.ReadLine());
                                                Console.WriteLine(adminService.GetAdminById(adminid));
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

                                                Console.WriteLine("Enter First Name (leave blank to keep existing): ");
                                                string firstNameInput = Console.ReadLine();

                                                Console.WriteLine("Enter Last Name (leave blank to keep existing): ");
                                                string lastNameInput = Console.ReadLine();

                                                Console.WriteLine("Enter Email (leave blank to keep existing): ");
                                                string emailInput = Console.ReadLine();

                                                Console.WriteLine("Enter Phone Number (leave blank to keep existing): ");
                                                string phoneNumberInput = Console.ReadLine();

                                                Console.WriteLine("Enter Username (leave blank to keep existing): ");
                                                string usernameInput = Console.ReadLine();

                                                Console.WriteLine("Enter Password (leave blank to keep existing): ");
                                                string passwordInput = Console.ReadLine();

                                                Console.WriteLine("Enter Role (leave blank to keep existing): ");
                                                string roleInput = Console.ReadLine();

                                                Console.WriteLine("Enter Join Date (leave blank to keep existing): ");
                                                string joinDateInput = Console.ReadLine();

                                                // Fetch the existing admin details
                                                Admin existingAdmin = adminService.GetAdminById(adminIdToUpdate);

                                                Admin updatedAdmin = new Admin()
                                                {
                                                    FirstName = !string.IsNullOrWhiteSpace(firstNameInput) ? firstNameInput : existingAdmin.FirstName,
                                                    LastName = !string.IsNullOrWhiteSpace(lastNameInput) ? lastNameInput : existingAdmin.LastName,
                                                    Email = !string.IsNullOrWhiteSpace(emailInput) ? emailInput : existingAdmin.Email,
                                                    PhoneNumber = !string.IsNullOrWhiteSpace(phoneNumberInput) ? phoneNumberInput : existingAdmin.PhoneNumber,
                                                    Username = !string.IsNullOrWhiteSpace(usernameInput) ? usernameInput : existingAdmin.Username,
                                                    Password = !string.IsNullOrWhiteSpace(passwordInput) ? passwordInput : existingAdmin.Password,
                                                    Role = !string.IsNullOrWhiteSpace(roleInput) ? roleInput : existingAdmin.Role,
                                                    JoinDate = !string.IsNullOrWhiteSpace(joinDateInput) ? DateTime.Parse(joinDateInput) : existingAdmin.JoinDate
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
                                Thread.Sleep(8000); 
                                continue; 
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("Invalid input format. Please enter valid data.");
                                Thread.Sleep(8000); 
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
                Customer loginCus1 = customerService.GetCustomerByUsername(cusUsername);
                if (cusPassword != loginCus1.Password)
                {
                    throw new AuthenticationException("Incorrect password.");
                }
                return loginCus1;
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


