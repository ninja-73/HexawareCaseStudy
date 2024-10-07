using CourierManagementSystem.MenuModule;
using CourierManagementSystem.Model;
using System;
using System.Text;
namespace CourierManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {

            #region Task1 Q1
            //1. Write a program that checks whether a given order is delivered or not based on its status (e.g.,"Processing," "Delivered," "Cancelled"). Use if-else statements for this.     
            //string status = "Shipped";
            //if (status == "Delivered")
            //{
            //    Console.WriteLine("The order is delivered");
            //}
            //else if (status == "In-Transit" || status == "Shipped" || status == "Pending")
            //{
            //    Console.WriteLine("The order is yet to be delivered");
            //}
            //else if (status == "Canceled")
            //{
            //    Console.WriteLine("The order is canceled");
            //}
            //else { Console.WriteLine("Enter valid input"); }
            #endregion

            #region Q2
            //2. Implement a switch-case statement to categorize parcels based on their weight into "Light," "Medium," or "Heavy."
            //double weight = 3.5;
            //string category = weight switch
            //{
            //    double w when w >= 0.0 && w <= 2.0 => "Light",
            //    double w when w >= 2.0 && w < 4.0 => "Medium",
            //    double w when w >= 4.0 && w < 7.0 => "Heavy",
            //    double w when w > 7.0 => "Overweight",
            //    _ => "Invalid weight"
            //};
            //Console.WriteLine(category);
            #endregion

            #region Q3
            //3. Implement User Authentication 1. Create a login system for employees and customers using Java control flow statements.
            //Console.WriteLine("Enter choice: 1.Register\t2.Login");
            //int ch = int.Parse(Console.ReadLine());
            //switch (ch)
            //{
            //    case 1:
            //        Console.WriteLine("Enter Username: ");
            //        string name = Console.ReadLine();
            //        Console.WriteLine("Enter Password");
            //        string password = Console.ReadLine();
            //        Console.WriteLine($"The account for Username {name} is successfully created!");
            //        break;
            //    case 2:
            //        Console.WriteLine("Enter Username: ");
            //        string lName = Console.ReadLine();
            //        Console.WriteLine("Enter Password");
            //        string lPassword = Console.ReadLine();
            //        Console.WriteLine($"Welcome {lName}!, You have successfully logged in");
            //        break;
            //}
            #endregion

            #region Q4
            //4. Implement Courier Assignment Logic 1. Develop a mechanism to assign couriers to shipments based on predefined criteria(e.g., proximity, load capacity) using loops.
            //string choice;
            //do
            //{
            //    Console.WriteLine("Enter courier ID: ");
            //    int courierId = int.Parse(Console.ReadLine());
            //    Console.WriteLine("Enter courier weight: ");
            //    int weight = int.Parse(Console.ReadLine());

            //    if (0 < weight && weight <= 5)
            //    {
            //        Console.WriteLine($"CourierID {courierId} is assigned to shipment 1");   
            //    }
            //    else if (5 < weight && weight <= 10)
            //    {
            //        Console.WriteLine($"CourierID {courierId} is assigned to shipment 2");
            //    }
            //    else if (10 < weight && weight < 15)
            //    {
            //        Console.WriteLine($"CourierID {courierId} is assigned to shipment 3");
            //    }
            //    else
            //    {
            //        Console.WriteLine("Invalid Input");
            //    }
            //    Console.WriteLine("Do you want to continue? (y/n)");
            //    choice = Console.ReadLine();
            //}
            //while (choice.Equals("y"));
            #endregion

            #region Task2 Q5
            //5.Write a C# program that uses a for loop to display all the orders for a specific customer. 
            //String[,] orders = new string[,]
            //{
            //    {"101", "OrderID: 1 Laptop"},
            //    {"102", "OrderID: 2 Mobile"},
            //    {"101", "OrderID: 3 Tablet"},
            //    {"103", "OrderID: 4 Headphones"},
            //    {"101", "OrderID: 5 TV"},
            //    {"102", "OrderID: 6 Monitor"}
            //};
            //Console.Write("Enter the Customer ID: ");
            //string customerId = Console.ReadLine();

            //Console.WriteLine($"\nOrders for Customer ID {customerId}:\n");
            //bool ordersFound = false;

            //for (int i = 0; i < (orders.GetLength(0)); i++)
            //{
            //    if (orders[i, 0] == customerId)
            //    {
            //        Console.WriteLine(orders[i, 1]);
            //        ordersFound = true;
            //    }
            //}
            //if (!ordersFound)
            //{
            //    Console.WriteLine($"No orders found for customer ID {customerId}");
            //}
            #endregion

            #region Q6
            //6. Implement a while loop to track the real-time location of a courier until it reaches its destination. 
            //int currentLocattion = 0;
            //int destination = 4;
            //while (currentLocattion < destination)
            //{
            //    Console.WriteLine($"The remaining distance is {destination - currentLocattion}");
            //    currentLocattion++;
            //}
            //Console.WriteLine("Reached");
            #endregion

            #region Task 3 Q7
            //7. Create an array to store the tracking history of a parcel, where each entry represents a location update
            //List<string> location = new List<string>();
            //string choice;
            //string loc;
            //do
            //{
            //    Console.WriteLine("Enter current location: ");
            //    loc = Console.ReadLine();
            //    location.Add(loc);
            //    Console.WriteLine("Do u want to continue? (y/n)");
            //    choice = Console.ReadLine();
            //}while (choice == "y");
            //Console.WriteLine($"The tracking history is:");
            //foreach (string item in location)
            //{
            //    Console.WriteLine(item);
            //}
            #endregion

            #region Q8

            //    string[] courierNames = new string[] { "Courier A", "Courier B", "Courier C", "Courier D" };
            //    double[] courierDistances = new double[] { 5.0, 3.2, 7.8, 2.5 };
            //    string nearestCourier = FindNearestCourier(courierNames, courierDistances);
            //    Console.WriteLine($"The nearest courier is {nearestCourier}.");
            //    #endregion
            //}
            //#region Method for Q8
            //public static string FindNearestCourier(string[] courierNames, double[] courierDistances)
            //{
            //    int nearest = 0;
            //    double minDistance = courierDistances[0];
            //    for (int i = 1; i < courierDistances.Length; i++)
            //    {
            //        if (courierDistances[i] < minDistance)
            //        {
            //            minDistance = courierDistances[i];
            //            nearest = i;
            //        }
            //    }
            //    return courierNames[nearest];
            //}
            #endregion

            #region Task4 Q9

            //string[,] parcels = new string[,]
            //    {
            //    { "1001", "In Transit" },
            //    { "1002", "Out for Delivery" },
            //    { "1003", "Delivered" },
            //    { "1004", "Processing" },
            //    { "1005", "Shipped" }
            //    };

            //Console.WriteLine("Enter the parcel tracking number:");
            //string trackingNumber = Console.ReadLine();

            //bool parcelFound = false;
            //for (int i = 0; i < parcels.GetLength(0); i++)
            //{
            //    if (parcels[i, 0] == trackingNumber)
            //    {
            //        string status = parcels[i, 1];
            //        parcelFound = true;

            //        switch (status)
            //        {
            //            case "In Transit":
            //                Console.WriteLine("Your parcel is currently in transit.");
            //                break;
            //            case "Out for Delivery":
            //                Console.WriteLine("Your parcel is out for delivery.");
            //                break;
            //            case "Delivered":
            //                Console.WriteLine("Your parcel has been delivered.");
            //                break;
            //            case "Processing":
            //                Console.WriteLine("Your parcel is being processed.");
            //                break;
            //            case "Shipped":
            //                Console.WriteLine("Your parcel has been shipped.");
            //                break;
            //            default:
            //                Console.WriteLine("Unknown status.");
            //                break;
            //        }
            //        break;
            //    }
            //}
            //if (!parcelFound)
            //{
            //    Console.WriteLine("Tracking number not found. Please try again.");
            //}
            #endregion

            #region Q10
            //    Console.WriteLine("Enter name: ");
            //    string name = Console.ReadLine();
            //    Console.WriteLine("Enter address: ");
            //    string address = Console.ReadLine();
            //    Console.WriteLine("Enter P.no: ");
            //    string ph = Console.ReadLine();
            //    string nValidation = Validation(1, name);
            //    string adValidation = Validation(2, address);
            //    string pValidation = Validation(3, ph);
            //    Console.WriteLine($"Name is {nValidation}, Address is {adValidation}, Phonenumber is {pValidation}");
            //    }
            //   
            //    static string Validation(int details, string data)
            //    {
            //        switch (details)
            //        {
            //            case 1:
            //                return NameValidation(data);
            //                break;
            //        case 2:
            //            return AddressValidation(data);
            //            break;
            //        case 3:
            //            return PhValidation(data);
            //            break;
            //        default:
            //                return "Invalid";
            //                break;
            //        }
            //    }
            //    static string NameValidation(string data)
            //    {
            //        if (!string.IsNullOrWhiteSpace(data) && data.All(char.IsLetter))
            //        {
            //            return "Validated";
            //        }
            //        else { return "Not Validated"; }
            //    }
            //    static string AddressValidation(string data)
            //    {
            //        if (data.Length >= 5 && data.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || (c.Equals(","))))
            //        {
            //            return "Validated";
            //    }
            //    else
            //    {
            //        return "Not Validated";
            //    }
            //    }
            //static string PhValidation(string data)
            //{
            //    string pattern = @"^\d{3}-\d{3}-\d{4}$";
            //    if (Regex.IsMatch(data, pattern))
            //    {
            //        return "Validated";
            //    }
            //    else
            //    {
            //        return "Not Validated";
            //    }
            //}
            #endregion

            #region Q11
            //    Console.Write("Enter street: ");
            //    string street = Console.ReadLine();
            //    Console.Write("Enter city: ");
            //    string city = Console.ReadLine();
            //    Console.Write("Enter state: ");
            //    string state = Console.ReadLine();
            //    Console.Write("Enter zip code: ");
            //    string zipcode = Console.ReadLine();
            //    Console.WriteLine(AddressFormat(street, city, state, zipcode)); 

            //}

            //static string AddressFormat(string street, string city, string state, string zipcode)
            //{
            //    string formattedStreet = $"{char.ToUpper(street[0])}{street.Substring(1,street.Length-1)}";
            //    string formattedCity = $"{char.ToUpper(city[0])}{city.Substring(1, city.Length - 1)}";
            //    string formattedState = $"{char.ToUpper(state[0])}{state.Substring(1, state.Length - 1)}";
            //    string formattedZipcode = $"{zipcode.Substring(0,3)}-{zipcode.Substring(3,3)}";
            //    return $"The formatted address: {formattedStreet}, {formattedCity}, {formattedState}, {formattedZipcode}";
            //}
            #endregion

            #region Q12
            //Console.WriteLine("Enter Name: ");
            //string name = Console.ReadLine();
            //Console.WriteLine("Enter Product: ");
            //string product = Console.ReadLine();
            //Console.WriteLine("Enter Address: ");
            //string address = Console.ReadLine();
            //DateTime orderedDate = DateTime.Now;
            //Console.WriteLine($"\nHi {name},\nYour product {product} will be delivered to {address} on/before {orderedDate.AddDays(4)}");
            #endregion

            #region Q13
            //    Console.WriteLine("Enter sender address: ");
            //    string sAddress = Console.ReadLine();
            //    Console.WriteLine("Enter receiver address: ");
            //    string rAddress = Console.ReadLine();
            //    Console.WriteLine("Enter weight: ");
            //    double weight = double.Parse(Console.ReadLine());
            //    Console.WriteLine(ShippingCost(sAddress, rAddress, weight));
            //}
            //    static double ShippingCost(string sAdd, string rAdd, double weight)
            //    {
            //        double distance = 10;
            //        const int costPerKm = 5;
            //        double distCost = distance* costPerKm;

            //        const int costPerKg = 10;
            //        double weightCost = weight * costPerKg;

            //        return (distCost + weightCost);
            //    }
            #endregion

            #region Q14
            //string upperCase = "ABCDEFGHIJKLMOPQRSTUVWXYZ";
            //string lowerCase = "abcdefghijklmopqrstuvwxyz";
            //string specialChars = "!@#$%&*_+=?/";
            //string numbers = "1234567890";

            //Console.WriteLine("Enter the length of password (must be > 6): ");
            //int len = int.Parse(Console.ReadLine());
            //StringBuilder sb = new StringBuilder();

            //Random random = new Random();

            //for (int i = 0; i < len/4; i++)
            //{
            //    sb.Append(upperCase[random.Next(upperCase.Length)]);
            //    sb.Append(lowerCase[random.Next(lowerCase.Length)]);
            //    sb.Append(specialChars[random.Next(specialChars.Length)]);
            //    sb.Append(numbers[random.Next(numbers.Length)]);
            //}

            //Console.WriteLine($"Your generated password is {sb.ToString()}");
            #endregion

            #region Q15
            //    var locations = new Dictionary<long, string>
            //    {
            //        { 1, "123 Elm Street" },
            //        { 2, "123 Elm St." },
            //        { 3, "456 Oak Avenue" },
            //        { 4, "789 Pine Road" },
            //        { 5, "123 Elm Street" }, // Duplicate address
            //        { 6, "456 Oak St." },
            //        { 7, "123 Elm Street Apt 1" },
            //        { 8, "456 Oak Avenue" }  // Duplicate address
            //    };

            //    // Call the method to find duplicated addresses
            //    FindDuplicateAddresses(locations);

            //}
            //static void FindDuplicateAddresses(Dictionary<long, string> locations)
            //{
            //    // A dictionary to store the addresses and their corresponding location IDs
            //    var addressMap = new Dictionary<string, List<long>>();

            //    // Populate the address map
            //    foreach (var location in locations)
            //    {
            //        if (!addressMap.ContainsKey(location.Value))
            //        {
            //            addressMap[location.Value] = new List<long>();
            //        }
            //        addressMap[location.Value].Add(location.Key);
            //    }

            //    // Print out duplicated addresses with their corresponding location IDs
            //    Console.WriteLine("Duplicate Addresses Found:");
            //    foreach (var entry in addressMap)
            //    {
            //        if (entry.Value.Count > 1) // More than one location ID means duplicate
            //        {
            //            Console.WriteLine($"- Address: {entry.Key}, Location IDs: {string.Join(", ", entry.Value)}");
            //        }
            //    }
            //}

            #endregion

            MainMenu menu = new MainMenu();
            menu.Run();
        }
    }
}   


