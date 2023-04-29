using BLL.Dtos;

namespace BLL.Services.Interfaces
{
    public interface IDishService : IService<DishDto>
    {
        DishDto Get(int id);
        IEnumerable<DishDto> GetDishesByMenu(int menuId);
        public IEnumerable<DishDto> GetByNameAll(string name);
        DishDto GetByName(string name);
        DishDto Create(CreateDishDto entity);
        DishDto AddIngredientToDish(int dishId, int ingredientId);
        void Delete(int id);
        DishDto DeleteIngredientFromDish(int dishId, int ingredientId);
    }
}
