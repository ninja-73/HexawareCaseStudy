using CarConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Repository.CustomerRepository
{
    internal interface ICustomerRepo
    {
        List<Customer> GetAllCustomers();
        Customer GetCustomerById(int customerId);
        Customer GetCustomerByUsername(string username);
        int RegisterCustomer(Customer customer);
        int UpdateCustomer(int id, Customer customer);
        int DeleteCustomer(int id);

    }
}
