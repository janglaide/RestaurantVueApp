using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class DishRepository : IDishRepository
    {
        private readonly RestaurantContext _context;

        public DishRepository(RestaurantContext context)
        {
            _context = context;
        }
        public IEnumerable<Dish> GetAll()
        {
            return _context.Dishes
                .Include(x => x.Portion);
        }

        public void Create(Dish entity)
        {
            _context.Add(entity);
        }

        public void Update(Dish entity)
        {
            _context.Update(entity);
        }

        public void Delete(Dish entity)
        {
            _context.Remove(entity);
        }

        public Dish Get(int id)
        {
            return _context.Dishes
                .Include(x => x.Portion)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
