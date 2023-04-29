using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.Windows
{
    /// <summary>
    /// Interaction logic for DishWindow.xaml
    /// </summary>
    public partial class DishWindow : Window
    {
        private readonly OrderService _orderService = new OrderService();

        private readonly IList<DishWithIngredients> _dishes;
        private readonly List<Button> _potionButtons = new List<Button>();
        private DishWithIngredients _currentView;
        public DishWindow(IList<DishWithIngredients> dishes)
        {
            _dishes = dishes;
            InitializeComponent();
            var firstDish = dishes.FirstOrDefault();

            if (firstDish is null)
            {
                ErrorLabel.Content = "Error occurred";
                return;
            }

            _currentView = firstDish;
            DishNameLabel.Content = firstDish.Name;
            PriceValueLabel.Content = $"{firstDish.Price:0.0} UAH";

            for (var i = 0; i < dishes.Count; i++)
            {
                var disable = dishes[i].PortionName.Equals(firstDish.PortionName);

                var portionButton = new Button()
                {
                    Content = dishes[i].PortionName,
                    IsEnabled = !disable,
                    Margin = new Thickness(225 + i * 80,
                        120,
                        this.Width - (225 + (i + 1) * 80),
                        this.Height - (120 + 70)),
                    Background = Brushes.LightSteelBlue,
                    BorderThickness = new Thickness(0),
                };
                portionButton.Click += ChangePortion_Click;
                _potionButtons.Add(portionButton);
                MainGrid.Children.Add(portionButton);
            }
        }

        private void ChangePortion_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var portionName = button!.Content.ToString();

            var dish = _dishes.FirstOrDefault(x => x.PortionName.Equals(portionName));
            var buttonToEnable = _potionButtons.FirstOrDefault(x => x.Content.Equals(_currentView.PortionName))
                .IsEnabled = true;
            var buttonToDisable = _potionButtons.FirstOrDefault(x => x.Content.Equals(portionName))
                .IsEnabled = false;

            DishNameLabel.Content = dish.Name;
            PriceValueLabel.Content = $"{dish.Price:0.0} UAH";
            _currentView = dish;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            window.Show();
            Close();
        }

        private void AddToOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var order = _orderService.AddDishToOrder(User.Instance.OrderId, _currentView.Id);
            if (User.Instance.OrderId == 0)
            {
                User.Instance.OrderId = order.Id;
            }
        }

        private void ViewOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new OrderWindow(User.Instance);
            window.Show();
            Close();
        }
    }
}
