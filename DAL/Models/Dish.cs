namespace DAL.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int PortionId { get; set; }
        public Portion Portion { get; set; }

        public ICollection<DishIngredient> DishIngredients { get; set; }
        public ICollection<OrderDish> OrderDishes { get; set; }
        public ICollection<MenuDish> MenuDishes { get; set; }
    }
}
