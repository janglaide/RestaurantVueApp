namespace BLL.Dtos
{
    public class DishIngredientDto
    {
        public int DishId { get; set; }
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public int IngredientQuantity { get; set; }
    }
}
