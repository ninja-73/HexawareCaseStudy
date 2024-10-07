using CourierManagementSystem.Model;
using CourierManagementSystem.Repository.User;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierManagementSystem.Service.UserService
{
    internal class CourierUserService:ICourierUserService
    {
        readonly ICourierUserRepo courierUserRepo;
        public CourierUserService()
        {
            courierUserRepo = new CourierUserRepo();
        }

        public void PlaceOrder(Courier courier)
        {
            int status = courierUserRepo.PlaceOrder(courier);
            if (status > 0)
            {
                Console.WriteLine("Ordered Placed");
            }
            else
            {
                Console.WriteLine("Ordered Failed!");
            }
        }

        public void GetOrderStatus(string tnumber)
        {
            string status1 = courierUserRepo.GetOrderStatus(tnumber);
            Console.WriteLine(status1);
        }

        public void CancelOrder(string tnumber)
        {
            bool status2 = courierUserRepo.CancelOrder(tnumber);
            Console.WriteLine(status2);
        }

        
    }
}
