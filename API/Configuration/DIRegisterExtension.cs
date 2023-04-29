using BLL.Services;
using BLL.Services.Interfaces;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using DAL.UOW;
namespace API.Configuration
{
    public static class DIRegisterExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IDishIngredientRepository, DishIngredientRepository>();
            services.AddTransient<IDishRepository, DishRepository>();
            services.AddTransient<IIngredientRepository, IngredientRepository>();
            services.AddTransient<IMenuDishRepository, MenuDishRepository>();
            services.AddTransient<IMenuRepository, MenuRepository>();
            services.AddTransient<IOrderDishRepository, OrderDishRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IPortionRepository, PortionRepository>();

            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IDishService, DishService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IDishIngredientService, DishIngredientService>();
            services.AddScoped<IIngredientService, IngredientService>();
        }
    }
}
