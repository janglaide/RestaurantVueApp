using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IOrderDishRepository : IRepository<OrderDish>
    {
        OrderDish Get(int orderId, int dishId);
    }
}
