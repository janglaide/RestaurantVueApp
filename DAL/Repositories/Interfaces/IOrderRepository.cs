using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order Get(int id);
    }
}
