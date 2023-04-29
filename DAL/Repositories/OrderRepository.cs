using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RestaurantContext _context;

        public OrderRepository(RestaurantContext context)
        {
            _context = context;
        }
        public IEnumerable<Order> GetAll()
        {
            return _context.Orders;
        }

        public void Create(Order entity)
        {
            _context.Add(entity);
        }

        public void Update(Order entity)
        {
            _context.Update(entity);
        }

        public void Delete(Order entity)
        {
            _context.Remove(entity);
        }

        public Order Get(int id)
        {
            return _context.Orders
                .Include(x => x.OrderDishes)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
