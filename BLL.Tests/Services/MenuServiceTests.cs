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
    public class MenuServiceTests
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
        public void Get_MenuExists_ReturnsItem()
        {
            //arrange
            var sut = GetSut();

            //act
            var result = sut.Get(1);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Default", result.Name);
        }

        [Test]
        public void Get_MenuDoesNotExist_ReturnsNull()
        {
            //arrange
            var sut = GetSut();

            //act
            var result = sut.Get(3);

            //assert
            Assert.IsNull(result);
        }

        [Test]
        public void CreateMenu_ReturnsCreatedItem()
        {
            //arrange

            var menuDto = new MenuDto()
            {
                Name = "Breakfast",
            };

            var menus = new[]
            {
                new Menu() {Id = 1, Name = "Default" },
                new Menu() {Id = 2, Name = "Kids" },
                new Menu() {Id = 3, Name = "Breakfast"},
            };

            var menuRepository = Substitute.For<IMenuRepository>();
            menuRepository.GetAll().Returns(menus);
            menuRepository.Get(1).Returns(menus[0]);

            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.MenuRepository.Returns(menuRepository);

            var mapper = Substitute.For<IMapper>();
            mapper.Map<MenuDto>(Arg.Any<Menu>()).Returns(new MenuDto()
            {
                Id = 3, 
                Name = "Breakfast",
            });

            var sut = GetSut(unitOfWork, mapper);

            //act
            var result = sut.CreateMenu(menuDto);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(menuDto.Name, result.Name);
        }

        [Test]
        public void AddDishToMenu_AddingNewDishToMenu_ReturnsMenu()
        {
            //arrange
            var sut = GetSut();
            var menuId = 1;
            var dishId = 2;

            //act
            var result = sut.AddDishToMenu(menuId, dishId);

            //assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void AddDishToMenu_DishAlreadyExistsInMenu_ReturnsMenu()
        {
            //arrange
            var sut = GetSut();
            var menuId = 1;
            var dishId = 1;

            //act
            var result = sut.AddDishToMenu(menuId, dishId);

            //assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetByName_MenuExists_ReturnsMenuDto()
        {
            //arrange
            var sut = GetSut();

            //act
            var result = sut.GetByName("Kids");

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Id);
        }

        [Test]
        public void GetByName_MenuDoesNotExist_ReturnsNull()
        {
            //arrange
            var sut = GetSut();

            //act
            var result = sut.GetByName("Party");

            //assert
            Assert.IsNull(result);
        }

        [Test]
        public void RemoveDishFromMenu_DishExistsInMenu_TheItemRemoved()
        {
            //arrange
            var sut = GetSut();
            var menuId = 1;
            var dishId = 1;

            //act
            var result = sut.RemoveDishFromMenu(menuId, dishId);

            //assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void RemoveDishFromMenu_DishDoesNotExistInMenu_ReturnsNull()
        {
            //arrange
            var sut = GetSut();
            var menuId = 1;
            var dishId = 2;

            //act
            var result = sut.RemoveDishFromMenu(menuId, dishId);

            //assert
            Assert.IsNull(result);
        }

        private MenuService GetSut(IUnitOfWork unitOfWork = null, IMapper mapper = null)
        {
            var menus = new[]
            {
                new Menu() {Id = 1, Name = "Default" },
                new Menu() {Id = 2, Name = "Kids" },
            };

            var menuDishes = new[]
            {
                new MenuDish() {MenuId = 1, DishId = 1},
            };

            if (unitOfWork is null)
            {
                var menuRepository = Substitute.For<IMenuRepository>();
                menuRepository.GetAll().Returns(menus);
                menuRepository.Get(1).Returns(menus[0]);

                var menuDishRepository = Substitute.For<IMenuDishRepository>();
                menuDishRepository.GetAll().Returns(menuDishes);
                menuDishRepository.Get(1, 1).Returns(menuDishes[0]);

                unitOfWork = Substitute.For<IUnitOfWork>();
                unitOfWork.MenuRepository.Returns(menuRepository);
                unitOfWork.MenuDishRepository.Returns(menuDishRepository);
            }

            if (mapper is null)
            {
                mapper = Substitute.For<IMapper>();
                mapper.Map<IEnumerable<MenuDto>>(Arg.Any<IEnumerable<Menu>>()).Returns(new List<MenuDto>()
                {
                    new MenuDto() {Id = 1, Name = "Default"},
                    new MenuDto() {Id = 2, Name = "Kids"},
                });
                mapper.Map<MenuDto>(menus[0]).Returns(new MenuDto()
                {
                    Id = menus[0].Id,
                    Name = menus[0].Name,
                });
                mapper.Map<MenuDto>(menus[1]).Returns(new MenuDto()
                {
                    Id = menus[1].Id,
                    Name = menus[1].Name,
                });
            }

            mapper.Map<Menu>(Arg.Any<MenuDto>()).Returns(new Menu()
            {
                Id = 3,
                Name = "Breakfast",
            });

            return new MenuService(unitOfWork, mapper);
        }
    }
}
