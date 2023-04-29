using DAL.Context.Seed;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class RestaurantContext : DbContext
    {
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuDish> MenuDishes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDish> OrderDishes { get; set; }
        public DbSet<Portion> Portions { get; set; }

        public RestaurantContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>().HasKey(dish => dish.Id);
            modelBuilder.Entity<Ingredient>().HasKey(ingredient => ingredient.Id);
            modelBuilder.Entity<DishIngredient>().HasKey(dishIngredient => new { dishIngredient.DishId, dishIngredient.IngredientId });
            modelBuilder.Entity<Order>().HasKey(order => order.Id);
            modelBuilder.Entity<OrderDish>().HasKey(orderDish => new { orderDish.DishId, orderDish.OrderId });
            modelBuilder.Entity<Menu>().HasKey(menu => menu.Id);
            modelBuilder.Entity<MenuDish>().HasKey(md => new { md.MenuId, md.DishId });
            modelBuilder.Entity<Portion>().HasKey(portion => portion.Id);

            modelBuilder.Entity<DishIngredient>()
                .HasOne(ds => ds.Dish)
                .WithMany(ds => ds.DishIngredients)
                .HasForeignKey(ds => ds.DishId);

            modelBuilder.Entity<DishIngredient>()
                .HasOne(ds => ds.Ingredient)
                .WithMany(ds => ds.DishIngredients)
                .HasForeignKey(ds => ds.IngredientId);

            modelBuilder.Entity<OrderDish>()
                .HasOne(od => od.Order)
                .WithMany(od => od.OrderDishes)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<OrderDish>()
                .HasOne(od => od.Dish)
                .WithMany(od => od.OrderDishes)
                .HasForeignKey(od => od.DishId);

            modelBuilder.Entity<MenuDish>()
                .HasOne(md => md.Menu)
                .WithMany(md => md.MenuDishes)
                .HasForeignKey(md => md.MenuId);

            modelBuilder.Entity<MenuDish>()
                .HasOne(md => md.Dish)
                .WithMany(md => md.MenuDishes)
                .HasForeignKey(md => md.DishId);

            modelBuilder.Entity<Dish>()
                .HasOne(d => d.Portion)
                .WithMany(d => d.Dishes)
                .HasForeignKey(d => d.PortionId);

            modelBuilder.Entity<Menu>().HasData(SeedDatabase.Menus);
            modelBuilder.Entity<Portion>().HasData(SeedDatabase.Portions);
            modelBuilder.Entity<Order>().HasData(SeedDatabase.Orders);
            modelBuilder.Entity<Ingredient>().HasData(SeedDatabase.Ingredients);
            modelBuilder.Entity<Dish>().HasData(SeedDatabase.Dishes);
            modelBuilder.Entity<DishIngredient>().HasData(SeedDatabase.DishIngredients);
            modelBuilder.Entity<MenuDish>().HasData(SeedDatabase.MenuDishes);
            modelBuilder.Entity<OrderDish>().HasData(SeedDatabase.OrderDishes);
        }
    }
}
