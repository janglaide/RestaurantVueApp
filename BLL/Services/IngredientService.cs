using AutoMapper;
using BLL.Dtos;
using BLL.Services.Interfaces;
using DAL.UOW;

namespace BLL.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public IngredientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<IngredientDto> GetAll()
        {
            var ingredients = _unitOfWork.IngredientRepository.GetAll();
            return _mapper.Map<IEnumerable<IngredientDto>>(ingredients);
        }

        public IngredientDto Get(int id)
        {
            var ingredient = _unitOfWork.IngredientRepository.Get(id);
            return _mapper.Map<IngredientDto>(ingredient);
        }
    }
}
