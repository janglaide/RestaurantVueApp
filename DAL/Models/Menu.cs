namespace DAL.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<MenuDish> MenuDishes { get; set; }
    }
}