using CourierManagementSystem.Model;

namespace CourierManagementSystem.Repository.User
{
    internal interface ICourierUserRepo
    {
        int PlaceOrder(Courier courier);

        string GetOrderStatus(string trackingNumber);

        bool CancelOrder(string trackingNumber);
    }
}
