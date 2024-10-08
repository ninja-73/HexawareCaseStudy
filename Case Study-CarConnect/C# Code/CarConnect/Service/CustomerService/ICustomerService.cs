using CarConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Service.CustomerService
{
    internal interface ICustomerService
    {
        Customer GetCustomerById(int id);
        Customer GetCustomerByUsername(string name);
        void RegisterCustomer(Customer customer);
        void UpdateCustomer(int id, Customer customer);
        void DeleteCustomer(int id);
    }
}
