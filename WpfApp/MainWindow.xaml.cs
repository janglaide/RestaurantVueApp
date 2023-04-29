using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp.Models;
using WpfApp.Services;
using WpfApp.Windows;
using Menu = WpfApp.Models.Menu;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MenuService _menuService = new MenuService();
        private readonly DishService _dishService = new DishService();

        private readonly List<Button> _menuButtons = new List<Button>();
        private Menu _currentMenu;
        public MainWindow()
        {
            InitializeComponent();
            var user = new User();

            var menus = _menuService.GetMenus();

            for (var i = 0; i < menus.Count; i++)
            {
                var button = new Button()
                {
                    Content = menus[i].Name,
                    Margin = new Thickness(200 + 100 * i, 106,
                        this.Width - (200 + 100 * (i + 1)),
                        276),
                    Background = Brushes.LightSteelBlue,
                    BorderThickness = new Thickness(0),
                };
                button.Click += MenuButton_Click;
                _menuButtons.Add(button);
                MainGrid.Children.Add(button);
            }

            _currentMenu = menus.FirstOrDefault();
            var buttonToDisable = _menuButtons.FirstOrDefault(x => x.Content.Equals(_currentMenu.Name))
                .IsEnabled = false;
            ListDishes();

            var disableOrderButton = User.Instance.OrderId != 0;
            OrderButton.IsEnabled = disableOrderButton;
        }

        private void ListDishes(int menuId = 1)
        {
            DishesGrid.Children.Clear();
            var dishes = _menuService.GetMenuWithDish(menuId).Dishes;

            dishes = dishes.DistinctBy(x => x.Name).ToList();
            
            for (var i = 0; i < dishes.Count; i++)
            {
                var button = new Button()
                {
                    Content = dishes[i].Name,
                    Margin = new Thickness(50, 
                        30 + i * 30,
                        50,
                        this.Height - (180 + i * 30)),
                    
                    BorderThickness = new Thickness(0),
                    Background = Brushes.DarkSeaGreen,
                    
                };
                button.Click += DishButton_Click;
                DishesGrid.Children.Add(button);
            }
        }

        private void DishButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var dishName = button!.Content.ToString();
            var dishesFound = _dishService.GetDishesByName(dishName);

            var window = new DishWindow(dishesFound);
            window.Show();
            Close();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var menuName = button!.Content.ToString();
            var menu = _menuService.GetMenuByName(menuName);
            var buttonToEnable = _menuButtons.FirstOrDefault(x => x.Content.Equals(_currentMenu.Name))
                .IsEnabled = true;
            var buttonToDisable = _menuButtons.FirstOrDefault(x => x.Content.Equals(menu.Name))
                .IsEnabled = false;
            _currentMenu = menu;
            ListDishes(menu.Id);
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new OrderWindow(User.Instance);
            window.Show();
            Close();
        }
    }
}
