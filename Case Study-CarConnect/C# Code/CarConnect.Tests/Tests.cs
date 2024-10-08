using CarConnect.MenuModule;
using CarConnect.Models;
using CarConnect.Service.VehicleService;

using NUnit.Framework;
using static CarConnect.Exceptions.CustomExceptions;
using CarConnect.Repository.VehicleRepository;

namespace CarConnect.Tests
{
    public class Tests
    {
        private CarConnectMenu carConnectMenu;
        private VehicleService vehicleService;
        private VehicleRepo vehicleRepo;

        [SetUp]
        public void Setup()
        {
            carConnectMenu = new CarConnectMenu();
            vehicleService = new VehicleService();
            vehicleRepo = new VehicleRepo();
        }

        [Test]
        public void AuthenticateCustomer_InvalidUsername()
        {
            // Arrange
            string invalidUsername = "invalidUser";
            string password = "password";

            // Act & Assert
            var ex = Assert.Throws<AuthenticationException>(() => carConnectMenu.AuthenticateCustomer(invalidUsername, password));
            Assert.That(ex.Message, Is.EqualTo("Username does not exist."));
        }

        [Test]
        public void AuthenticateCustomer_InvalidPassword()
        {
            // Arrange
            string validUsername = "john_doe";
            string invalidPassword = "wrongPassword";

            // Act & Assert
            var ex = Assert.Throws<AuthenticationException>(() => carConnectMenu.AuthenticateCustomer(validUsername, invalidPassword));
            Assert.That(ex.Message, Is.EqualTo("Incorrect password."));
        }

        [Test]
        [Ignore("Before Running Test change the RegistrationNumber as it has unique constraint in DB")]
        public void AddVehicle()
        {
            var vehicle = new Vehicle
            {
                Model = "Honda",
                Make = "Civic",
                Year = 2023,
                Color = "Blue",
                RegistrationNumber = "ND9ew876",
                Availability = 1,
                DailyRate = 60.00m
            };

            vehicleService.AddVehicle(vehicle);

            Vehicle latestVehicle = vehicleService.GetLatestVehicle();
            Assert.That(latestVehicle.Model, Is.EqualTo(vehicle.Model), "Model should match");
            Assert.That(latestVehicle.Make, Is.EqualTo(vehicle.Make), "Make should match");
            Assert.That(latestVehicle.Year, Is.EqualTo(vehicle.Year), "Year should match");
            Assert.That(latestVehicle.Color, Is.EqualTo(vehicle.Color), "Color should match");
            Assert.That(latestVehicle.RegistrationNumber, Is.EqualTo(vehicle.RegistrationNumber), "Registration Number should match");
            Assert.That(latestVehicle.Availability, Is.EqualTo(vehicle.Availability), "Availability should match");
            Assert.That(latestVehicle.DailyRate, Is.EqualTo(vehicle.DailyRate), "Daily Rate should match");
        }

        [Test]
        public void UpdateVehicle()
        {
            int vehicleId = 4; 

            Vehicle updatedVehicle = new Vehicle
            {
                VehicleId = vehicleId,
                Model = "Honda",
                Make = "Accord",
                Year = 2023,
                Color = "Red",
                RegistrationNumber = "HND1234",
                Availability = 0,
                DailyRate = 70.00m
            };

            int updateStatus = vehicleRepo.UpdateVehicle(vehicleId, updatedVehicle);

           Vehicle latestVehicle = vehicleRepo.GetVehicleById(vehicleId);

            Assert.That(latestVehicle.Model, Is.EqualTo(updatedVehicle.Model), "Model should match");
            Assert.That(latestVehicle.Make, Is.EqualTo(updatedVehicle.Make), "Make should match");
            Assert.That(latestVehicle.Year, Is.EqualTo(updatedVehicle.Year), "Year should match");
            Assert.That(latestVehicle.Color, Is.EqualTo(updatedVehicle.Color), "Color should match");
            Assert.That(latestVehicle.RegistrationNumber, Is.EqualTo(updatedVehicle.RegistrationNumber), "Registration Number should match");
            Assert.That(latestVehicle.Availability, Is.EqualTo(updatedVehicle.Availability), "Availability should match");
            Assert.That(latestVehicle.DailyRate, Is.EqualTo(updatedVehicle.DailyRate), "Daily Rate should match");
        }


        [Test]
        public void GetAllVehicles()
        {
            List<Vehicle> vehicles = vehicleRepo.GetAllVehicles();

            Assert.That(vehicles, Is.Not.Null, "Vehicle list should not be null");
            Assert.That(vehicles, Is.Not.Empty, "Vehicle list should not be empty");
        }

        [Test]
        public void GetAvailableVehicles()
        {
            List<Vehicle> availableVehicles = vehicleRepo.GetAvailableVehicles();

            Assert.That(availableVehicles, Is.Not.Null, "Available vehicles list should not be null");
            Assert.That(availableVehicles, Is.Not.Empty, "Available vehicles list should not be empty");

            foreach (var vehicle in availableVehicles)
            {
                Assert.That(vehicle.Availability, Is.EqualTo(1), "Vehicle should be available");
            }

        }
    }
}
