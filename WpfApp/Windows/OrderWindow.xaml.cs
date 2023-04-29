using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.Windows
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private readonly User _user;
        private readonly OrderService _orderService = new OrderService();
        private Order _currentOrder;
        public OrderWindow(User user)
        {
            InitializeComponent();
            _user = user;

            _currentOrder = _orderService.Get(_user.OrderId);
            PopulateWindow();
        }

        private void PopulateWindow()
        {
            DishesGrid.Children.Clear();

            OrderIdLabel.Content = $"Id: {_currentOrder.Id}";
            PriceLabel.Content = $"Price: {_currentOrder.Price:0.00UAH}";

            for (var i = 0; i < _currentOrder.Dishes.Count; i++)
            {
                var dishNameLabel = new Label()
                {
                    Content = _currentOrder.Dishes[i].Name,
                    FontSize = 14,
                    Margin = new Thickness(20,
                        30 + i * 40,
                        Width - 185,
                        Height - (80 + i * 40)),
                };

                var dishPortionLabel = new Label()
                {
                    Content = _currentOrder.Dishes[i].PortionName,
                    FontSize = 14,
                    Margin = new Thickness(200,
                        30 + i * 40,
                        Width - 295,
                        Height - (80 + i * 40)),
                };

                var dishPriceLabel = new Label()
                {
                    Content = $"{_currentOrder.Dishes[i].Price:0.00UAH}",
                    FontSize = 14,
                    Margin = new Thickness(300,
                        30 + i * 40,
                        Width - 390,
                        Height - (80 + i * 40)),
                };

                var quantityLabel = new Label()
                {
                    Content = $"{_currentOrder.Dishes[i].Quantity} item(s)",
                    FontSize = 14,
                    Margin = new Thickness(400,
                        30 + i * 40,
                        Width - 500,
                        Height - (80 + i * 40)),
                };
                var sortedChars = _currentOrder.Dishes[i].Name
                    .ToCharArray()
                    .Where(x => !char.IsWhiteSpace(x))
                    .ToArray();

                var name = new string(sortedChars);
                var addMoreButton = new Button()
                {
                    Name = $"AddMoreButton_{name}{_currentOrder.Dishes[i].PortionId}",
                    Content = "Add more",
                    Margin = new Thickness(520,
                        30 + i * 40,
                        Width - 620,
                        Height - (80 + i * 40)),
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.DarkSeaGreen,
                    Background = Brushes.Transparent,
                };

                var removeButton = new Button()
                {
                    Name = $"AddMoreButton_{name}{_currentOrder.Dishes[i].PortionId}",
                    Content = "Remove",
                    Margin = new Thickness(650,
                        30 + i * 40,
                        Width - 750,
                        Height - (80 + i * 40)),
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.PaleVioletRed,
                    Background = Brushes.Transparent,
                };
                addMoreButton.Click += AddMoreButton_Click;
                removeButton.Click += RemoveFromOrder_Click;

                DishesGrid.Children.Add(dishNameLabel);
                DishesGrid.Children.Add(dishPortionLabel);
                DishesGrid.Children.Add(dishPriceLabel);
                DishesGrid.Children.Add(quantityLabel);
                DishesGrid.Children.Add(addMoreButton);
                DishesGrid.Children.Add(removeButton);
            }
        }

        private void AddMoreButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var dish = _currentOrder.Dishes
                .Where(x => button.Name
                    .Contains(
                        new string(x.Name
                            .ToCharArray()
                            .Where(n => !char.IsWhiteSpace(n))
                            .ToArray())
                    ))
                .FirstOrDefault(x => button.Name.Contains(x.PortionId.ToString()));
            if (dish == null)
            {
                return;
            }

            var updatedOrder = _orderService.AddDishToOrder(_currentOrder.Id, dish.Id);
            _currentOrder = updatedOrder;
            PopulateWindow();
        }

        private void RemoveFromOrder_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var dish = _currentOrder.Dishes
                .Where(x => button.Name
                    .Contains(
                        new string(x.Name
                            .ToCharArray()
                            .Where(n => !char.IsWhiteSpace(n))
                            .ToArray())
                    ))
                .FirstOrDefault(x => button.Name.Contains(x.PortionId.ToString()));

            if (dish == null)
            {
                return;
            }

            var updatedOrder = _orderService.RemoveDishFromOrder(_currentOrder.Id, dish.Id);
            _currentOrder = updatedOrder;
            PopulateWindow();
        }

        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            window.Show();
            Close();
        }
    }
}
