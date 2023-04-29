using System.Collections.Generic;

namespace WpfApp.Models
{
    public class DishWithIngredients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PortionName { get; set; }
        public List<DishIngredient> IngredientsList { get; set; } = new List<DishIngredient>();
    }
}
