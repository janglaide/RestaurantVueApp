using BLL.Dtos;

namespace BLL.Services.Interfaces
{
    public interface IIngredientService : IService<IngredientDto>
    {
        public IngredientDto Get(int id);
    }
}
