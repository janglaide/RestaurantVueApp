using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IDishIngredientRepository : IRepository<DishIngredient>
    {
        DishIngredient Get(int dishId, int ingredientId);
    }
}
