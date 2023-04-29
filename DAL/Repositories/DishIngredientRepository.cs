using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class DishIngredientRepository : IDishIngredientRepository
    {
        private readonly RestaurantContext _context;

        public DishIngredientRepository(RestaurantContext context)
        {
            _context = context;
        }
        public IEnumerable<DishIngredient> GetAll()
        {
            return _context.DishIngredients
                .Include(x => x.Ingredient);
        }

        public void Create(DishIngredient entity)
        {
            _context.Add(entity);
        }

        public void Update(DishIngredient entity)
        {
            _context.Update(entity);
        }

        public void Delete(DishIngredient entity)
        {
            _context.Remove(entity);
        }

        public DishIngredient Get(int dishId, int ingredientId)
        {
            return _context.DishIngredients.FirstOrDefault(x => x.DishId == dishId && x.IngredientId == ingredientId);
        }
    }
}
