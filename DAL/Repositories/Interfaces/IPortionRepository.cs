using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IPortionRepository :IRepository<Portion>
    {
        Portion Get(int id);
    }
}
