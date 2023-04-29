using DAL.Models;

namespace DAL.Context.Seed
{
    public class SeedDatabase
    {
        public static IEnumerable<Menu> Menus => new Menu[]
        {
            new Menu() {Id = 1, Name = "Default menu"},
            new Menu() {Id = 2, Name = "Kids menu"},
        };

        public static IEnumerable<Portion> Portions => new Portion[]
        {
            new Portion() {Id = 1, Name = "Simple"},
            new Portion() {Id = 2, Name = "Double"},
            new Portion() {Id = 3, Name = "Spicy"},
        };

        public static IEnumerable<Ingredient> Ingredients => new Ingredient[]
        {
            new Ingredient() {Id = 1, Name = "Flour"},
            new Ingredient() {Id = 2, Name = "Tomato"},
            new Ingredient() {Id = 3, Name = "Cheese"},
            new Ingredient() {Id = 4, Name = "Egg"},
            new Ingredient() {Id = 5, Name = "Onion"},
            new Ingredient() {Id = 6, Name = "Pineapple"},
            new Ingredient() {Id = 7, Name = "Cucumber"},
            new Ingredient() {Id = 8, Name = "Salad leaves"},
            new Ingredient() {Id = 9, Name = "Basil"},
            new Ingredient() {Id = 10, Name = "Sweet Pepper"},
            new Ingredient() {Id = 11, Name = "Chili Pepper"},
            new Ingredient() {Id = 12, Name = "Potato"},
            new Ingredient() {Id = 13, Name = "Beef"},
        };

        public static IEnumerable<Dish> Dishes => new Dish[]
        {
            new Dish() {Id = 1, Name = "Hawaii pizza", PortionId = 1, Price = 200},
            new Dish() {Id = 2, Name = "Hawaii pizza", PortionId = 2, Price = 350},
            new Dish() {Id = 3, Name = "Hawaii pizza", PortionId = 3, Price = 250},
            new Dish() {Id = 4, Name = "Green salad", PortionId = 1, Price = 110},
            new Dish() {Id = 5, Name = "Fried eggs", PortionId = 1, Price = 145},
            new Dish() {Id = 6, Name = "Steak Filet Mignon", PortionId = 1, Price = 470},
            new Dish() {Id = 7, Name = "Tomato soup", PortionId = 1, Price = 125},
            new Dish() {Id = 8, Name = "Tomato soup", PortionId = 3, Price = 125},
            new Dish() {Id = 9, Name = "Bolognese pasta", PortionId = 1, Price = 150},
            new Dish() {Id = 10, Name = "Bolognese pasta", PortionId = 2, Price = 210},
        };

        public static IEnumerable<OrderDish> OrderDishes => new OrderDish[]
        {
            new OrderDish() {OrderId = 1, DishId = 3, Quantity = 1},
            new OrderDish() {OrderId = 1, DishId = 4, Quantity = 1},
            new OrderDish() {OrderId = 1, DishId = 7, Quantity = 2},
        };

        public static IEnumerable<Order> Orders => new Order[]
        {
            new Order() {Id = 1, Price = 610},
        };

        public static IEnumerable<MenuDish> MenuDishes => new MenuDish[]
        {
            new MenuDish() {MenuId = 1, DishId = 1},
            new MenuDish() {MenuId = 1, DishId = 2},
            new MenuDish() {MenuId = 1, DishId = 3},
            new MenuDish() {MenuId = 1, DishId = 4},
            new MenuDish() {MenuId = 1, DishId = 5},
            new MenuDish() {MenuId = 1, DishId = 6},
            new MenuDish() {MenuId = 1, DishId = 7},
            new MenuDish() {MenuId = 1, DishId = 8},
            new MenuDish() {MenuId = 1, DishId = 9},
            new MenuDish() {MenuId = 1, DishId = 10},
            new MenuDish() {MenuId = 2, DishId = 1},
            new MenuDish() {MenuId = 2, DishId = 2},
            new MenuDish() {MenuId = 2, DishId = 4},
            new MenuDish() {MenuId = 2, DishId = 7},
            new MenuDish() {MenuId = 2, DishId = 9},
        };

        public static IEnumerable<DishIngredient> DishIngredients => new DishIngredient[]
        {
            new DishIngredient() {DishId = 10, IngredientId = 1, IngredientQuantity = 2},
            new DishIngredient() {DishId = 10, IngredientId = 2, IngredientQuantity = 4},
            new DishIngredient() {DishId = 10, IngredientId = 4, IngredientQuantity = 2},
            new DishIngredient() {DishId = 10, IngredientId = 9, IngredientQuantity = 1},
            new DishIngredient() {DishId = 10, IngredientId = 13, IngredientQuantity = 2},

            new DishIngredient() {DishId = 9, IngredientId = 1, IngredientQuantity = 1},
            new DishIngredient() {DishId = 9, IngredientId = 2, IngredientQuantity = 2},
            new DishIngredient() {DishId = 9, IngredientId = 4, IngredientQuantity = 1},
            new DishIngredient() {DishId = 9, IngredientId = 9, IngredientQuantity = 1},
            new DishIngredient() {DishId = 9, IngredientId = 13, IngredientQuantity = 1},

            new DishIngredient() {DishId = 8, IngredientId = 2, IngredientQuantity = 5},
            new DishIngredient() {DishId = 8, IngredientId = 5, IngredientQuantity = 3},
            new DishIngredient() {DishId = 8, IngredientId = 10, IngredientQuantity = 2},
            new DishIngredient() {DishId = 8, IngredientId = 12, IngredientQuantity = 2},
            new DishIngredient() {DishId = 8, IngredientId = 11, IngredientQuantity = 3},

            new DishIngredient() {DishId = 7, IngredientId = 2, IngredientQuantity = 5},
            new DishIngredient() {DishId = 7, IngredientId = 5, IngredientQuantity = 1},
            new DishIngredient() {DishId = 7, IngredientId = 10, IngredientQuantity = 2},
            new DishIngredient() {DishId = 7, IngredientId = 12, IngredientQuantity = 2},

            new DishIngredient() {DishId = 6, IngredientId = 13, IngredientQuantity = 1},
            new DishIngredient() {DishId = 6, IngredientId = 11, IngredientQuantity = 1},
            new DishIngredient() {DishId = 6, IngredientId = 12, IngredientQuantity = 1},

            new DishIngredient() {DishId = 5, IngredientId = 2, IngredientQuantity = 1},
            new DishIngredient() {DishId = 5, IngredientId = 4, IngredientQuantity = 2},
            new DishIngredient() {DishId = 5, IngredientId = 5, IngredientQuantity = 1},
            new DishIngredient() {DishId = 5, IngredientId = 9, IngredientQuantity = 1},

            new DishIngredient() {DishId = 4, IngredientId = 2, IngredientQuantity = 2},
            new DishIngredient() {DishId = 4, IngredientId = 3, IngredientQuantity = 1},
            new DishIngredient() {DishId = 4, IngredientId = 5, IngredientQuantity = 1},
            new DishIngredient() {DishId = 4, IngredientId = 7, IngredientQuantity = 2},
            new DishIngredient() {DishId = 4, IngredientId = 8, IngredientQuantity = 2},
            new DishIngredient() {DishId = 4, IngredientId = 9, IngredientQuantity = 1},
            new DishIngredient() {DishId = 4, IngredientId = 10, IngredientQuantity = 1},

            new DishIngredient() {DishId = 1, IngredientId = 1, IngredientQuantity = 1},
            new DishIngredient() {DishId = 1, IngredientId = 2, IngredientQuantity = 2},
            new DishIngredient() {DishId = 1, IngredientId = 3, IngredientQuantity = 1},
            new DishIngredient() {DishId = 1, IngredientId = 4, IngredientQuantity = 1},
            new DishIngredient() {DishId = 1, IngredientId = 5, IngredientQuantity = 1},
            new DishIngredient() {DishId = 1, IngredientId = 6, IngredientQuantity = 1},
            new DishIngredient() {DishId = 1, IngredientId = 9, IngredientQuantity = 1},

            new DishIngredient() {DishId = 2, IngredientId = 1, IngredientQuantity = 2},
            new DishIngredient() {DishId = 2, IngredientId = 2, IngredientQuantity = 2},
            new DishIngredient() {DishId = 2, IngredientId = 3, IngredientQuantity = 2},
            new DishIngredient() {DishId = 2, IngredientId = 4, IngredientQuantity = 2},
            new DishIngredient() {DishId = 2, IngredientId = 5, IngredientQuantity = 1},
            new DishIngredient() {DishId = 2, IngredientId = 6, IngredientQuantity = 1},
            new DishIngredient() {DishId = 2, IngredientId = 9, IngredientQuantity = 2},

            new DishIngredient() {DishId = 3, IngredientId = 1, IngredientQuantity = 1},
            new DishIngredient() {DishId = 3, IngredientId = 2, IngredientQuantity = 2},
            new DishIngredient() {DishId = 3, IngredientId = 3, IngredientQuantity = 1},
            new DishIngredient() {DishId = 3, IngredientId = 4, IngredientQuantity = 1},
            new DishIngredient() {DishId = 3, IngredientId = 5, IngredientQuantity = 2},
            new DishIngredient() {DishId = 3, IngredientId = 6, IngredientQuantity = 1},
            new DishIngredient() {DishId = 3, IngredientId = 9, IngredientQuantity = 1},
            new DishIngredient() {DishId = 3, IngredientId = 11, IngredientQuantity = 1},
        };
    }
}
