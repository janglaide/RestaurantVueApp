using AutoMapper;
using BLL.Dtos;
using BLL.Services.Interfaces;
using DAL.UOW;

namespace BLL.Services
{
    public class DishIngredientService : IDishIngredientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DishIngredientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<DishIngredientDto> GetAll()
        {
            var dishIngredients = _unitOfWork.DishIngredientRepository.GetAll();
            return _mapper.Map<IEnumerable<DishIngredientDto>>(dishIngredients);
        }

        public IEnumerable<DishIngredientDto> GetByDish(int dishId)
        {
            var allDtos = GetAll();
            return allDtos.Where(x => x.DishId == dishId);
        }
    }
}
