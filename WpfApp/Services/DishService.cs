using System.Collections.Generic;
using WpfApp.Client;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class DishService
    {
        private readonly DishClient _client;

        public DishService()
        {
            _client = new DishClient();
        }

        public IList<Dish> GetDishes()
        {
            var dishes = _client.GetList<Dish>();

            return dishes;
        }

        public IList<DishWithIngredients> GetDishesByName(string name)
        {
            var dishes = _client.GetByName(name);

            return dishes;
        }
    }
}
