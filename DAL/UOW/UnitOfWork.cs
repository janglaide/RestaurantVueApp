using DAL.Context;
using DAL.Repositories;
using DAL.Repositories.Interfaces;

namespace DAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RestaurantContext _context;

        private IDishIngredientRepository _dishIngredientRepository;
        private IDishRepository _dishRepository;
        private IIngredientRepository _ingredientRepository;
        private IMenuDishRepository _menuDishRepository;
        private IMenuRepository _menuRepository;
        private IOrderDishRepository _orderDishRepository;
        private IOrderRepository _orderRepository;
        private IPortionRepository _portionRepository;
        public UnitOfWork(RestaurantContext context)
        {
            _context = context;
        }

        public IDishIngredientRepository DishIngredientRepository => _dishIngredientRepository ??= new DishIngredientRepository(_context);
        public IDishRepository DishRepository => _dishRepository ??= new DishRepository(_context);
        public IIngredientRepository IngredientRepository => _ingredientRepository ??= new IngredientRepository(_context);
        public IMenuDishRepository MenuDishRepository => _menuDishRepository ??= new MenuDishRepository(_context);
        public IMenuRepository MenuRepository => _menuRepository ??= new MenuRepository(_context);
        public IOrderDishRepository OrderDishRepository => _orderDishRepository ??= new OrderDishRepository(_context);
        public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_context);
        public IPortionRepository PortionRepository => _portionRepository ??= new PortionRepository(_context);
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
