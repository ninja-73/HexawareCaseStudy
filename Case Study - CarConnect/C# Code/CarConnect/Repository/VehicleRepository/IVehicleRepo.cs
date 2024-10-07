using CarConnect.Models;

namespace CarConnect.Repository.VehicleRepository
{
    internal interface IVehicleRepo
    {
        Vehicle GetLatestVehicle();
        List<Vehicle> GetAllVehicles();
        Vehicle GetVehicleById(int vehicleId);
        List<Vehicle> GetAvailableVehicles();
        int AddVehicle(Vehicle vehicle);
        int UpdateVehicle(int id, Vehicle vehicle);
        int RemoveVehicle(int id);
    }
}
