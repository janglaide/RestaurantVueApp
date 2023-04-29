using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly RestaurantContext _context;

        public IngredientRepository(RestaurantContext context)
        {
            _context = context;
        }
        public IEnumerable<Ingredient> GetAll()
        {
            return _context.Ingredients;
        }

        public void Create(Ingredient entity)
        {
            _context.Add(entity);
        }

        public void Update(Ingredient entity)
        {
            _context.Update(entity);
        }

        public void Delete(Ingredient entity)
        {
            _context.Remove(entity);
        }

        public Ingredient Get(int id)
        {
            return _context.Ingredients.FirstOrDefault(x => x.Id == id);
        }
    }
}
