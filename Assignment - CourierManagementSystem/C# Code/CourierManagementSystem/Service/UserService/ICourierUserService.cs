using CourierManagementSystem.Model;

namespace CourierManagementSystem.Service.UserService
{
    internal interface ICourierUserService
    {
        void PlaceOrder(Courier courier);
        void GetOrderStatus(string tnumber);
        void CancelOrder(string  tnumber);
    }
}
