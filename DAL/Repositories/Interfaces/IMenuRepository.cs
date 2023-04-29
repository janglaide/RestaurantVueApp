using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Menu Get(int id);
    }
}
