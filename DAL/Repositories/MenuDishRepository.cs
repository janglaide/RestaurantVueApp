using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories
{
    public class MenuDishRepository : IMenuDishRepository
    {
        private readonly RestaurantContext _context;

        public MenuDishRepository(RestaurantContext context)
        {
            _context = context;
        }
        public IEnumerable<MenuDish> GetAll()
        {
            return _context.MenuDishes;
        }

        public void Create(MenuDish entity)
        {
            _context.MenuDishes.Add(entity);
        }

        public void Update(MenuDish entity)
        {
            _context.MenuDishes.Update(entity);
        }

        public void Delete(MenuDish entity)
        {
            _context.Remove(entity);
        }

        public MenuDish Get(int menuId, int dishId)
        {
            return _context.MenuDishes.FirstOrDefault(x => x.DishId == dishId && x.MenuId == menuId);
        }
    }
}
