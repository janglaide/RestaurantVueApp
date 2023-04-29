using System.Collections.Generic;

namespace WpfApp.Models
{
    public class MenuDishes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Dish> Dishes { get; set; } = new List<Dish>();
    }
}
