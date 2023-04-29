using AutoMapper;
using BLL.Dtos;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.UOW;

namespace BLL.Services
{
    public class DishService : IDishService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DishService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<DishDto> GetAll()
        {
            var dishes = _unitOfWork.DishRepository.GetAll().ToList();
            return _mapper.Map<IEnumerable<DishDto>>(dishes);
        }

        public DishDto Get(int id)
        {
            var dish = _unitOfWork.DishRepository.Get(id);
            return _mapper.Map<DishDto>(dish);
        }

        public IEnumerable<DishDto> GetDishesByMenu(int menuId)
        {
            var menuDishesIds = _unitOfWork.MenuDishRepository.GetAll()
                .Where(x => x.MenuId == menuId)
                .Select(x => x.DishId).ToList();

            var dishes = new List<DishDto>();
            foreach (var id in menuDishesIds)
            {
                var dish = _unitOfWork.DishRepository.Get(id);
                var dishDto = _mapper.Map<DishDto>(dish);
                dishes.Add(dishDto);
            }

            return dishes;
        }

        public IEnumerable<DishDto> GetByNameAll(string name)
        {
            var dishes = _unitOfWork.DishRepository.GetAll()
                .Where(x => x.Name.Equals(name));

            return dishes is null || !dishes.Any() ? null : _mapper.Map<IEnumerable<DishDto>>(dishes);
        }

        public DishDto GetByName(string name)
        {
            var dish = _unitOfWork.DishRepository.GetAll()
                .FirstOrDefault(x => x.Name.Equals(name));

            return dish is null ? null : _mapper.Map<DishDto>(dish);
        }

        public DishDto Create(CreateDishDto entity)
        {
            var dish = _mapper.Map<Dish>(entity);
            _unitOfWork.DishRepository.Create(dish);
            _unitOfWork.SaveChanges();

            var createdDish = _unitOfWork.DishRepository.GetAll().LastOrDefault();
            return _mapper.Map<DishDto>(createdDish);
        }

        public DishDto AddIngredientToDish(int dishId, int ingredientId)
        {
            var dishIngredient = _unitOfWork.DishIngredientRepository.Get(dishId, ingredientId);

            if (dishIngredient is null)
            {
                var createdDishIngredient = new DishIngredient()
                {
                    DishId = dishId,
                    IngredientId = ingredientId,
                    IngredientQuantity = 1,
                };
                _unitOfWork.DishIngredientRepository.Create(createdDishIngredient);
            }
            else
            {
                dishIngredient.IngredientQuantity++;
            }
            _unitOfWork.SaveChanges();

            return Get(dishId);
        }

        public void Delete(int id)
        {
            var dish = _unitOfWork.DishRepository.Get(id);

            var dishIngredients = _unitOfWork.DishIngredientRepository.GetAll()
                .Where(x => x.DishId == id);
            foreach (var di in dishIngredients)
            {
                _unitOfWork.DishIngredientRepository.Delete(di);
            }

            var menuDishes = _unitOfWork.MenuDishRepository.GetAll()
                .Where(x => x.DishId == id);
            foreach (var md in menuDishes)
            {
                _unitOfWork.MenuDishRepository.Delete(md);
            }

            var orderDishes = _unitOfWork.OrderDishRepository.GetAll()
                .Where(x => x.DishId == id);
            foreach (var od in orderDishes)
            {
                _unitOfWork.OrderDishRepository.Delete(od);
            }

            _unitOfWork.DishRepository.Delete(dish);
            _unitOfWork.SaveChanges();
        }

        public DishDto DeleteIngredientFromDish(int dishId, int ingredientId)
        {
            var dishIngredient = _unitOfWork.DishIngredientRepository.Get(dishId, ingredientId);
            if (dishIngredient is null)
            {
                return null;
            }

            if (dishIngredient.IngredientQuantity > 1)
            {
                dishIngredient.IngredientQuantity--;
                _unitOfWork.DishIngredientRepository.Update(dishIngredient);
            }
            else
            {
                _unitOfWork.DishIngredientRepository.Delete(dishIngredient);
            }
            _unitOfWork.SaveChanges();

            return Get(dishId);
        }
    }
}
