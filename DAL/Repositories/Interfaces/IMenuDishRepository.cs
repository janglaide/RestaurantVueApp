using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IMenuDishRepository : IRepository<MenuDish>
    {
        MenuDish Get(int menuId, int dishId);
    }
}
