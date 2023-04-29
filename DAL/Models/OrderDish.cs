namespace DAL.Models
{
    public class OrderDish
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int DishId { get; set; }
        public Dish Dish { get; set; }
        public int Quantity { get; set; }
    }
}
