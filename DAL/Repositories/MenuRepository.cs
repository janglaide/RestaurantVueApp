using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly RestaurantContext _context;

        public MenuRepository(RestaurantContext context)
        {
            _context = context;
        }
        public Menu Get(int id)
        {
            return _context.Menus.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Menu> GetAll()
        {
            return _context.Menus;
        }

        public void Create(Menu entity)
        {
            _context.Add(entity);
        }

        public void Update(Menu entity)
        {
            _context.Update(entity);
        }

        public void Delete(Menu entity)
        {
            _context.Remove(entity);
        }
    }
}
