using System.Collections.Generic;

namespace WpfApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public IList<OrderDish> Dishes { get; set; } = new List<OrderDish>();
    }
}
