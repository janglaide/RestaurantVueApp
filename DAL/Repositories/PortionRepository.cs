using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories
{
    public class PortionRepository : IPortionRepository
    {
        private readonly RestaurantContext _context;

        public PortionRepository(RestaurantContext context)
        {
            _context = context;
        }
        public IEnumerable<Portion> GetAll()
        {
            return _context.Portions;
        }

        public void Create(Portion entity)
        {
            _context.Add(entity);
        }

        public void Update(Portion entity)
        {
            _context.Update(entity);
        }

        public void Delete(Portion entity)
        {
            _context.Remove(entity);
        }

        public Portion Get(int id)
        {
            return _context.Portions.FirstOrDefault(x => x.Id == id);
        }
    }
}
