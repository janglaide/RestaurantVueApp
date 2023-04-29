namespace BLL.Dtos
{
    public class CreateDishDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int PortionId { get; set; }
    }
}
