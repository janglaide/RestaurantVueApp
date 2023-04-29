using BLL.Dtos;

namespace BLL.Services.Interfaces
{
    public interface IDishIngredientService : IService<DishIngredientDto>
    {
        public IEnumerable<DishIngredientDto> GetByDish(int dishId);
    }
}
