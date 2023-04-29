using API.ViewModels;
using AutoMapper;
using BLL.Dtos;
using DAL.Models;

namespace API.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Menu, MenuDto>().ReverseMap();
            CreateMap<Dish, DishDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Dish, GetOrderDishDto>().ReverseMap();
            CreateMap<DishIngredient, DishIngredientDto>().ReverseMap();
            CreateMap<Dish, CreateDishDto>().ReverseMap();
            CreateMap<Ingredient, IngredientDto>().ReverseMap();

            CreateMap<MenuDto, MenuViewModel>().ReverseMap();
            CreateMap<DishDto, DishViewModel>().ReverseMap();
            CreateMap<DishDto, DishWithIngredientsViewModel>().ReverseMap();
            CreateMap<MenuDto, MenuDishesViewModel>().ReverseMap();
        }
    }
}
