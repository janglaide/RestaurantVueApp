namespace API.ViewModels
{
    public class MenuDishesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DishViewModel> Dishes { get; set; } = new List<DishViewModel>();
    }
}
