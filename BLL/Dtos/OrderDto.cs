namespace BLL.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public IList<GetOrderDishDto> Dishes { get; set; } = new List<GetOrderDishDto>();
    }
}
