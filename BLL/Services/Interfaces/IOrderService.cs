using BLL.Dtos;

namespace BLL.Services.Interfaces
{
    public interface IOrderService : IService<OrderDto>
    {
        OrderDto Get(int id);
        OrderDto AddDish(int orderId, int dishId);
        void Remove(int id);
        OrderDto RemoveDish(int orderId, int dishId);
    }
}
