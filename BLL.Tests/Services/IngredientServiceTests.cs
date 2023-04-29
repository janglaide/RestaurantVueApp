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
    public class IngredientServiceTests
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
            var result = sut.Get(1);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Apple", result.Name);
        }

        private IngredientService GetSut()
        {
            var dbItems = new[]
            {
                new Ingredient() {Id = 1, Name = "Apple"},
                new Ingredient() {Id = 2, Name = "Tomato"},
            };

            var ingredientRepository = Substitute.For<IIngredientRepository>();
            ingredientRepository.GetAll().Returns(dbItems);
            ingredientRepository.Get(1).Returns(dbItems[0]);

            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.IngredientRepository.Returns(ingredientRepository);

            var mapper = Substitute.For<IMapper>();
            mapper.Map<IEnumerable<IngredientDto>>(dbItems).Returns(new List<IngredientDto>()
            {
                new IngredientDto() {Id = 1, Name = "Apple"},
                new IngredientDto() {Id = 2, Name = "Tomato"},
            });
            mapper.Map<IngredientDto>(dbItems[0]).Returns(new IngredientDto()
            {
                Id = dbItems[0].Id,
                Name = dbItems[0].Name,
            });

            return new IngredientService(unitOfWork, mapper);
        }
    }
}
