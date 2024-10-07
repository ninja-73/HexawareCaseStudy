using CarConnect.Models;
using CarConnect.Repository.CustomerRepository;
using CarConnect.Repository.VehicleRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CarConnect.Exceptions.CustomExceptions;

namespace CarConnect.Service.VehicleService
{
    internal class VehicleService : IVehicleService
    {
        readonly IVehicleRepo vehicleRepo;

        public VehicleService(IVehicleRepo vehicleRepo)
        {
            this.vehicleRepo = vehicleRepo;
        }
        public VehicleService() 
        {
            vehicleRepo = new VehicleRepo();
        }

        public Vehicle GetLatestVehicle()
        {
            return vehicleRepo.GetLatestVehicle();
        }

        public void GetAllVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            vehicles = vehicleRepo.GetAllVehicles();
            foreach (Vehicle vehicle in vehicles)
            {
                Console.WriteLine(vehicle);
            }
        }

        public void GetVehicleById(int id)
        {
            Vehicle vehicle = vehicleRepo.GetVehicleById(id);
            if (vehicle == null)
            {
                throw new VehicleNotFoundException($"Vehicle with ID {id} was not found.");
            }
            Console.WriteLine(vehicle);
        }

        public void GetAvailableVehicles()
        {
            List<Vehicle> vehicles = vehicleRepo.GetAvailableVehicles();
            foreach (Vehicle vehicle in vehicles)
            {
                Console.WriteLine(vehicle);
            }
        }

        public void AddVehicle(Vehicle vehicle)
        {
            int addStatus = vehicleRepo.AddVehicle(vehicle);
            if (addStatus > 0)
            {
                Console.WriteLine("Vehicle added Successfully");
            }
            else
            {
                Console.WriteLine("Addition Failed");
            }
        }

        public void UpdateVehicle(int id, Vehicle vehicle)
        {
            int Status = vehicleRepo.UpdateVehicle(id, vehicle);
            if (Status > 0)
            {
                Console.WriteLine("Vehicle updated Successfully");
            }
            else
            {
                Console.WriteLine("Updation Failed");
            }
        }

        public void RemoveVehicle(int id)
        {
            int Status = vehicleRepo.RemoveVehicle(id);
            if (Status > 0)
            {
                Console.WriteLine("Vehicle deleted Successfully");
            }
            else
            {
                Console.WriteLine("Deleted Failed");
            }
        }
    }
}
