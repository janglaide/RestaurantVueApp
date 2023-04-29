namespace WpfApp.Models
{
    public class OrderDish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int PortionId { get; set; }
        public string PortionName { get; set; }
        public int Quantity { get; set; }
    }
}
