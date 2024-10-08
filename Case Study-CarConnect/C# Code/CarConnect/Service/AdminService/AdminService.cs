using CarConnect.Models;
using CarConnect.Repository.AdminRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Service.AdminService
{
    internal class AdminService : IAdminService
    {
        readonly IAdminRepo adminRepo;

        public AdminService()
        {
            adminRepo = new AdminRepo();
        }

        public Admin GetAdminById(int id)
        {
            Admin admin = adminRepo.GetAdminById(id);
            return admin;
        }

        public Admin GetAdminByUsername(string username)
        {
            Admin admin = adminRepo.GetAdminByUsername(username);
            return admin;
        }

        public void RegisterAdmin(Admin admin)
        {
            int registerStatus = adminRepo.RegisterAdmin(admin);
            if (registerStatus > 0)
            {
                Console.WriteLine("Admin added Successfully");
            }
            else
            {
                Console.WriteLine("Addition Failed");
            }
        }

        public void UpdateAdmin(int id, Admin admin)
        {
            int updateStatus = adminRepo.UpdateAdmin(id, admin);
            if (updateStatus > 0)
            {
                Console.WriteLine("Admin updated Successfully");
            }
            else
            {
                Console.WriteLine("Updation Failed");
            }
        }

        public void DeleteAdmin(int id)
        {
            int deleteStatus = adminRepo.DeleteAdmin(id);
            if (deleteStatus > 0)
            {
                Console.WriteLine("Admin deleted Successfully");
            }
            else
            {
                Console.WriteLine("Deletion Failed");
            }
        }
    }
}
