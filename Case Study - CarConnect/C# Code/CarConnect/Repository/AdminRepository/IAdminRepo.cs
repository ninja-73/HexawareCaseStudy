using CarConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Repository.AdminRepository
{
    internal interface IAdminRepo
    {
        List<Admin> GetAllAdmins();
        Admin GetAdminById(int adminId);
        Admin GetAdminByUsername(string username);
        int RegisterAdmin(Admin adminData);
        int UpdateAdmin(int id, Admin adminData);
        int DeleteAdmin(int adminId);

    }
}
