namespace DAL.Models
{
    public class Portion
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Dish> Dishes { get; set; }
    }
}
