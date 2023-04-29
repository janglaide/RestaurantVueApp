using System.Collections.Generic;
using WpfApp.Client;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class MenuService
    {
        private readonly GenericClient _client;

        public MenuService()
        {
            _client = new GenericClient();
        }

        public IList<Menu> GetMenus()
        {
            var menuList = _client.GetList<Menu>();

            return menuList;
        }

        public MenuDishes GetMenuWithDish(int id)
        {
            var menu = _client.Get<MenuDishes>(id);

            return menu;
        }

        public Menu GetMenuByName(string name)
        {
            var menu = _client.GetByName<Menu>(name);

            return menu;
        }
    }
}
