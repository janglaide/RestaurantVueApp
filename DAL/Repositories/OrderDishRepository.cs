using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories
{
    public class OrderDishRepository : IOrderDishRepository
    {
        private readonly RestaurantContext _context;

        public OrderDishRepository(RestaurantContext context)
        {
            _context = context;
        }
        public IEnumerable<OrderDish> GetAll()
        {
            return _context.OrderDishes;
        }

        public void Create(OrderDish entity)
        {
            _context.Add(entity);
        }

        public void Update(OrderDish entity)
        {
            _context.Update(entity);
        }

        public void Delete(OrderDish entity)
        {
            _context.Remove(entity);
        }

        public OrderDish Get(int orderId, int dishId)
        {
            return _context.OrderDishes.FirstOrDefault(x => x.OrderId == orderId && x.DishId == dishId);
        }
    }
}
