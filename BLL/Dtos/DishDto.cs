namespace BLL.Dtos
{
    public class DishDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int PortionId { get; set; }
        public string PortionName { get; set; }
    }
}
