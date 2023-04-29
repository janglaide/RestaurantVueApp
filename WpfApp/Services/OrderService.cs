using WpfApp.Client;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class OrderService
    {
        private readonly OrderClient _orderClient;

        public OrderService()
        {
            _orderClient = new OrderClient();
        }

        public Order Get(int id)
        {
            return _orderClient.Get<Order>(id);
        }

        public Order AddDishToOrder(int orderId, int dishId)
        {
            return _orderClient.AddDishToOrder(orderId, dishId);
        }

        public Order RemoveDishFromOrder(int orderId, int dishId)
        {
            return _orderClient.RemoveDishFromOrder(orderId, dishId);
        }
    }
}
