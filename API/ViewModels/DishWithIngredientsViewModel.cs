using BLL.Dtos;

namespace API.ViewModels
{
    public class DishWithIngredientsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PortionName { get; set; }
        public List<DishIngredientDto> IngredientsList { get; set; } = new List<DishIngredientDto>();
    }
}
