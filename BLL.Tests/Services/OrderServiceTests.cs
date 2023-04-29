using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.Dtos;
using BLL.Services;
using DAL.Models;
using DAL.Repositories.Interfaces;
using DAL.UOW;
using NSubstitute;
using NUnit.Framework;

namespace BLL.Tests.Services
{
    [TestFixture]
    public class OrderServiceTests
    {
        [Test]
        public void GetAll_HavingItems_ReturnsAllItems()
        {
            //arrange
            var sut = GetSut();

            //act
            var result = sut.GetAll();

            //assert
            Assert.IsNotEmpty(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void Get_OrderExists_ReturnsItem()
        {
            //arrange
            var sut = GetSut();

            //act
            var result = sut.Get(1);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(100, result.Price);
        }

        [Test]
        public void Get_OrderDoesNotExist_ReturnsNull()
        {
            //arrange
            var sut = GetSut();

            //act
            var result = sut.Get(3);
            
            //assert
            Assert.IsNull(result);
        }

        [Test]
        public void RemoveDish_OneDishInOrder_ReturnsOrder()
        {
            //arrange
            var sut = GetSut();
            var orderId = 1;
            var dishId = 1;
            //act
            var result = sut.RemoveDish(orderId, dishId);

            //assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void RemoveDish_TwoDishInOrder_ReturnsOrder()
        {
            //arrange
            var sut = GetSut();
            var orderId = 1;
            var dishId = 2;
            //act
            var result = sut.RemoveDish(orderId, dishId);

            //assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void RemoveDish_NoDishInOrder_ReturnsNull()
        {
            //arrange
            var sut = GetSut();
            var orderId = 2;
            var dishId = 2;
            //act
            var result = sut.RemoveDish(orderId, dishId);

            //assert
            Assert.IsNull(result);
        }

        private OrderService GetSut(IUnitOfWork unitOfWork = null, IMapper mapper = null)
        {
            var orders = new[]
            {
                new Order() {Id = 1, Price = 100, OrderDishes = new List<OrderDish>()
                {
                    new OrderDish() {DishId = 1, OrderId = 1, Quantity = 1},
                }},
                new Order() {Id = 2, Price = 200},
                new Order() {Id = 0, Price = 100},
            };

            var dishes = new[]
            {
                new Dish() {Id = 1, Name = "Soup", Price = 100},
                new Dish() {Id = 2, Name = "Pizza", Price = 200},
            };

            var orderDishes = new[]
            {
                new OrderDish() {DishId = 1, OrderId = 1, Quantity = 1},
                new OrderDish() {DishId = 2, OrderId = 1, Quantity = 2},
            };

            if (unitOfWork is null)
            {
                var ordersRepository = Substitute.For<IOrderRepository>();
                ordersRepository.GetAll().Returns(orders);
                ordersRepository.Get(1).Returns(orders[0]);

                var dishRepository = Substitute.For<IDishRepository>();
                dishRepository.GetAll().Returns(dishes);
                dishRepository.Get(1).Returns(dishes[0]);
                dishRepository.Get(2).Returns(dishes[1]);

                var orderDishRepository = Substitute.For<IOrderDishRepository>();
                orderDishRepository.GetAll().Returns(orderDishes);

                unitOfWork = Substitute.For<IUnitOfWork>();
                unitOfWork.OrderRepository.Returns(ordersRepository);
                unitOfWork.DishRepository.Returns(dishRepository);
                unitOfWork.OrderDishRepository.Returns(orderDishRepository);
            }

            if (mapper is null)
            {
                mapper = Substitute.For<IMapper>();
                mapper.Map<IEnumerable<OrderDto>>(Arg.Any<IEnumerable<Order>>()).Returns(new List<OrderDto>()
                {
                    new OrderDto() {Id = 1, Price = 100},
                    new OrderDto() {Id = 2, Price = 200},
                });
                mapper.Map<IEnumerable<GetOrderDishDto>>(Arg.Any<IEnumerable<Dish>>()).Returns(new List<GetOrderDishDto>()
                {
                    new GetOrderDishDto() {Id = 1, Price = 100},
                    new GetOrderDishDto() {Id = 2, Price = 200},
                });
                mapper.Map<OrderDto>(orders[0]).Returns(new OrderDto()
                {
                    Id = orders[0].Id,
                    Price = orders[0].Price,
                });
                mapper.Map<OrderDto>(orders[1]).Returns(new OrderDto()
                {
                    Id = orders[1].Id,
                    Price = orders[1].Price,
                });
                mapper.Map<OrderDto>(orders[2]).Returns(new OrderDto()
                {
                    Id = orders[2].Id,
                    Price = orders[2].Price,
                });
            }

            return new OrderService(mapper, unitOfWork);
        }
    }
}
