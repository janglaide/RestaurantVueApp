using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IDishRepository : IRepository<Dish>
    {
        Dish Get(int id);
    }
}
