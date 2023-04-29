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
    public class DishIngredientServiceTests
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
        public void GetByDish_IngredientsExistByDish_ReturnsItems()
        {
            //arrange
            var sut = GetSut();

            //act
            var result = sut.GetByDish(1);

            //assert
            Assert.IsNotEmpty(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void GetByDish_IngredientsDoNotExistByDish_ReturnsEmptyResult()
        {
            //arrange
            var sut = GetSut();

            //act
            var result = sut.GetByDish(2);

            //assert
            Assert.IsEmpty(result);
        }

        private DishIngredientService GetSut()
        {
            var dbItems = new[]
            {
                new DishIngredient() {DishId = 1, IngredientId = 1, IngredientQuantity = 1},
                new DishIngredient() {DishId = 1, IngredientId = 2, IngredientQuantity = 2},
            };

            var dishIngredientRepository = Substitute.For<IDishIngredientRepository>();
            dishIngredientRepository.GetAll().Returns(dbItems);

            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.DishIngredientRepository.Returns(dishIngredientRepository);

            var mapper = Substitute.For<IMapper>();
            mapper.Map<IEnumerable<DishIngredientDto>>(dbItems).Returns(new List<DishIngredientDto>()
            {
                new DishIngredientDto() {DishId = 1, IngredientId = 1, IngredientQuantity = 1},
                new DishIngredientDto() {DishId = 1, IngredientId = 2, IngredientQuantity = 2},
            });

            return new DishIngredientService(unitOfWork, mapper);
        }
    }
}
