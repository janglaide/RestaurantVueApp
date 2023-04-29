using DAL.Repositories.Interfaces;

namespace DAL.UOW
{
    public interface IUnitOfWork
    {
        IDishIngredientRepository DishIngredientRepository { get; }
        IDishRepository DishRepository { get; }
        IIngredientRepository IngredientRepository { get; }
        IMenuDishRepository MenuDishRepository { get; }
        IMenuRepository MenuRepository { get; }
        IOrderDishRepository OrderDishRepository { get; }
        IOrderRepository OrderRepository { get; }
        IPortionRepository PortionRepository { get; }
        void SaveChanges();
    }
}
