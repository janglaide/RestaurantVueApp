using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
        Ingredient Get(int id);
    }
}
