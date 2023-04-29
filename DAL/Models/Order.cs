namespace DAL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public ICollection<OrderDish> OrderDishes { get; set; }
    }
}
