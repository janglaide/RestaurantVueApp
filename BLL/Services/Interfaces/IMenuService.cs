using BLL.Dtos;

namespace BLL.Services.Interfaces
{
    public interface IMenuService : IService<MenuDto>
    {
        MenuDto Get(int id);
        MenuDto CreateMenu(MenuDto menuDto);
        MenuDto AddDishToMenu(int menuId, int dishId);
        MenuDto GetByName(string name);
        void Delete(int id);
        MenuDto RemoveDishFromMenu(int menuId, int dishId);
    }
}
