using CarConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Service.AdminService
{
    internal interface IAdminService
    {
        Admin GetAdminById(int adminId);
        Admin GetAdminByUsername(string username);
        void RegisterAdmin(Admin adminData);
        void UpdateAdmin(int id, Admin admin);
        void DeleteAdmin(int adminId);
    }
}
