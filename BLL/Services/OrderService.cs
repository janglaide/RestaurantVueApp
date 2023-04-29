using AutoMapper;
using BLL.Dtos;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.UOW;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<OrderDto> GetAll()
        {
            var orders = _unitOfWork.OrderRepository.GetAll();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public OrderDto Get(int id)
        {
            var order = _unitOfWork.OrderRepository.Get(id);
            var orderDto = _mapper.Map<OrderDto>(order);
            if (orderDto is not null)
            {
                var allDishes = _unitOfWork.DishRepository.GetAll();
                var allDishesDtos = _mapper.Map<IEnumerable<GetOrderDishDto>>(allDishes);

                var dishesInOrder = order.OrderDishes
                    .Select(x => new {dish = x.DishId, q = x.Quantity});

                foreach (var item in dishesInOrder)
                {
                    var dish = allDishesDtos.FirstOrDefault(x => x.Id == item.dish);
                    dish.Quantity = item.q;
                    orderDto.Dishes.Add(dish);
                }
            }

            return orderDto;
        }

        public OrderDto AddDish(int orderId, int dishId)
        {
            var dish = _unitOfWork.DishRepository.Get(dishId);

            var order = _unitOfWork.OrderRepository.Get(orderId);

            if (order is not null)
            {
                var orderDishes = _unitOfWork.OrderDishRepository.GetAll()
                    .Where(x => x.OrderId == orderId);

                var existingDishInOrder = orderDishes.FirstOrDefault(x => x.DishId == dishId);
                if (existingDishInOrder is not null)
                {
                    existingDishInOrder.Quantity++;
                    _unitOfWork.OrderDishRepository.Update(existingDishInOrder);
                }
                else
                {
                    existingDishInOrder = new OrderDish() {DishId = dishId, OrderId = orderId, Quantity = 1};
                    _unitOfWork.OrderDishRepository.Create(existingDishInOrder);
                }
                order.Price += dish.Price;
                _unitOfWork.OrderRepository.Update(order);
            }
            else
            {
                order = new Order() { Price = 0 };
                _unitOfWork.OrderRepository.Create(order);
                _unitOfWork.SaveChanges();

                var orderInDb = _unitOfWork.OrderRepository.GetAll().LastOrDefault();

                orderId = orderInDb.Id;

                var dishInOrder = new OrderDish() {DishId = dishId, OrderId = orderId, Quantity = 1};
                _unitOfWork.OrderDishRepository.Create(dishInOrder);

                order.Price += dish.Price;
                _unitOfWork.OrderRepository.Update(order);
            }

            _unitOfWork.SaveChanges();
            return Get(orderId);
        }

        public void Remove(int id)
        {
            var orderDishes = _unitOfWork.OrderDishRepository.GetAll()
                .Where(x => x.OrderId == id);
            foreach (var item in orderDishes)
            {
                _unitOfWork.OrderDishRepository.Delete(item);
            }

            var order = _unitOfWork.OrderRepository.Get(id);
            _unitOfWork.OrderRepository.Delete(order);
            _unitOfWork.SaveChanges();
        }

        public OrderDto RemoveDish(int orderId, int dishId)
        {
            var order = _unitOfWork.OrderRepository.Get(orderId);

            var dish = _unitOfWork.DishRepository.Get(dishId);

            var orderDish = _unitOfWork.OrderDishRepository.GetAll()
                .FirstOrDefault(x => x.OrderId == orderId && x.DishId == dishId);

            if (orderDish is null)
            {
                return null;
            }

            if (orderDish.Quantity > 1)
            {
                orderDish.Quantity--;
                _unitOfWork.OrderDishRepository.Update(orderDish);
            }
            else
            {
                _unitOfWork.OrderDishRepository.Delete(orderDish);
            }

            order.Price -= dish.Price;
            _unitOfWork.OrderRepository.Update(order);
            _unitOfWork.SaveChanges();

            return Get(orderId);
        }
    }
}
