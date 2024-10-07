using CarConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Service.VehicleService
{
    internal interface IVehicleService
    {
        void GetAllVehicles();
        Vehicle GetLatestVehicle();
        void GetVehicleById(int id);
        void GetAvailableVehicles();
        void AddVehicle(Vehicle vehicle);
        void UpdateVehicle(int id, Vehicle vehicle);
        void RemoveVehicle(int id);
    }
}
