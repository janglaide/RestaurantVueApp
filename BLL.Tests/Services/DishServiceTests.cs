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
    public class DishServiceTests
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
        public void Get_DishExists_ReturnsItem()
        {
            //arrange
            var sut = GetSut();

            //act
            var result = sut.Get(1);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Soup", result.Name);
        }

        [Test]
        public void Get_DishDoesNotExist_ReturnsNull()
        {
            //arrange
            var sut = GetSut();

            //act
            var result = sut.Get(3);

            //assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetDishesByMenu_DishAndMenuExists_ReturnsItems()
        {
            //arrange
            var sut = GetSut();
            var menuId = 1;

            //act
            var result = sut.GetDishesByMenu(menuId);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void GetDishesByMenu_NoMenuExistsWithGivenId_ReturnsEmptyResult()
        {
            //arrange
            var sut = GetSut();
            var menuId = 2;

            //act
            var result = sut.GetDishesByMenu(menuId);

            //assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetByName_DishExists_ReturnsDishDto()
        {
            //arrange
            var sut = GetSut();

            //act
            var result = sut.GetByName("Pizza");

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Id);
        }

        [Test]
        public void GetByName_DishDoesNotExist_ReturnsNull()
        {
            //arrange
            var sut = GetSut();

            //act
            var result = sut.GetByName("Mivina");

            //assert
            Assert.IsNull(result);
        }

        [Test]
        public void Create_ReturnsCreatedITem()
        {
            //arrange
            var createDto = new CreateDishDto()
            {
                Name = "Salad"
            };

            var dishes = new[]
            {
                new Dish() {Id = 1, Name = "Soup"},
                new Dish() {Id = 2, Name = "Pizza"},
                new Dish() {Id = 3, Name = "Salad"},
            };
            var dishRepository = Substitute.For<IDishRepository>();
            dishRepository.GetAll().Returns(dishes);

            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.DishRepository.Returns(dishRepository);

            var mapper = Substitute.For<IMapper>();
            mapper.Map<DishDto>(dishes[2]).Returns(new DishDto()
            {
                Id = 3, 
                Name = "Salad",
            });

            var sut = GetSut(unitOfWork, mapper);

            //act
            var result = sut.Create(createDto);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(createDto.Name, result.Name);
        }

        [Test]
        public void AddIngredientToDish_AddingNewIngredientToTheDish()
        {
            //arrange
            var sut = GetSut();
            var dishId = 1;
            var ingredientId = 1;

            //act
            var result = sut.AddIngredientToDish(dishId, ingredientId);

            //assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void AddIngredientToDish_AddingExistingIngredientToTheDish()
        {
            //arrange
            var sut = GetSut();
            var dishId = 1;
            var ingredientId = 2;

            //act
            var result = sut.AddIngredientToDish(dishId, ingredientId);

            //assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void DeleteIngredientFromDish_DishExistsOneIngredient_TheItemRemoved()
        {
            //arrange
            var sut = GetSut();
            var dishId = 1;
            var ingredientId = 2;

            //act
            var result = sut.DeleteIngredientFromDish(dishId, ingredientId);

            //assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void DeleteIngredientFromDish_DishExistsTwoIngredients_IngredientsQuantityReduced()
        {
            //arrange
            var sut = GetSut();
            var dishId = 1;
            var ingredientId = 3;

            //act
            var result = sut.DeleteIngredientFromDish(dishId, ingredientId);

            //assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void DeleteIngredientFromDish_DishIngredientDoesNotExist_ReturnsNull()
        {
            //arrange
            var sut = GetSut();
            var dishId = 1;
            var ingredientId = 1;

            //act
            var result = sut.DeleteIngredientFromDish(dishId, ingredientId);

            //assert
            Assert.IsNull(result);
        }

        private DishService GetSut(IUnitOfWork unitOfWork = null, IMapper mapper = null)
        {
            var dishes = new[]
            {
                new Dish() {Id = 1, Name = "Soup", Price = 100},
                new Dish() {Id = 2, Name = "Pizza", Price = 200},
            };

            var menuDishes = new[]
            {
                new MenuDish() {MenuId = 1, DishId = 1},
            };

            if (unitOfWork is null)
            {
                var dishRepository = Substitute.For<IDishRepository>();
                dishRepository.GetAll().Returns(dishes);
                dishRepository.Get(1).Returns(dishes[0]);

                var menuDishRepository = Substitute.For<IMenuDishRepository>();
                menuDishRepository.GetAll().Returns(menuDishes);

                var dishIngredientRepository = Substitute.For<IDishIngredientRepository>();
                dishIngredientRepository.Get(1, 1).Returns((DishIngredient)null);
                dishIngredientRepository.Get(1, 2).Returns(new DishIngredient()
                {
                    DishId = 1, 
                    IngredientId = 2,
                    IngredientQuantity = 1,
                });
                dishIngredientRepository.Get(1, 3).Returns(new DishIngredient()
                {
                    DishId = 1,
                    IngredientId = 3,
                    IngredientQuantity = 2,
                });

                unitOfWork = Substitute.For<IUnitOfWork>();
                unitOfWork.DishRepository.Returns(dishRepository);
                unitOfWork.MenuDishRepository.Returns(menuDishRepository);
                unitOfWork.DishIngredientRepository.Returns(dishIngredientRepository);
            }

            if (mapper is null)
            {
                mapper = Substitute.For<IMapper>();
                mapper.Map<IEnumerable<DishDto>>(Arg.Any<IEnumerable<Dish>>()).Returns(new List<DishDto>()
                {
                    new DishDto() {Id = 1, Name = "Soup", Price = 100},
                    new DishDto() {Id = 2, Name = "Pizza", Price = 200},
                });
                mapper.Map<DishDto>(dishes[0]).Returns(new DishDto()
                {
                    Id = dishes[0].Id,
                    Name = dishes[0].Name,
                    Price = dishes[0].Price,
                });
                mapper.Map<DishDto>(dishes[1]).Returns(new DishDto()
                {
                    Id = dishes[1].Id,
                    Name = dishes[1].Name,
                    Price = dishes[1].Price,
                });
            }

            mapper.Map<Dish>(Arg.Any<CreateDishDto>()).Returns(new Dish()
            {
                Id = 3,
                Name = "Salad",
                Price = 50,
            });

            return new DishService(mapper, unitOfWork);
        }
    }
}
