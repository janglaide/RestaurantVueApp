using AutoMapper;
using BLL.Dtos;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.UOW;

namespace BLL.Services
{
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MenuService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<MenuDto> GetAll()
        {
            var menus = _unitOfWork.MenuRepository.GetAll();
            return _mapper.Map<IEnumerable<MenuDto>>(menus);
        }

        public MenuDto Get(int id)
        {
            var menu = _unitOfWork.MenuRepository.Get(id);
            return _mapper.Map<MenuDto>(menu);
        }

        public MenuDto CreateMenu(MenuDto menuDto)
        {
            var menu = _mapper.Map<Menu>(menuDto);
            _unitOfWork.MenuRepository.Create(menu);
            _unitOfWork.SaveChanges();

            var lastMenu = _unitOfWork.MenuRepository.GetAll().LastOrDefault();
            return _mapper.Map<MenuDto>(lastMenu);
        }

        public MenuDto AddDishToMenu(int menuId, int dishId)
        {
            var menu = _unitOfWork.MenuRepository.Get(menuId);
            var menuDish = _unitOfWork.MenuDishRepository.Get(menuId, dishId);

            if (menuDish is not null)
            {
                return _mapper.Map<MenuDto>(menu);
            }

            var createdMenuDish = new MenuDish() {MenuId = menuId, DishId = dishId};
            _unitOfWork.MenuDishRepository.Create(createdMenuDish);
            _unitOfWork.SaveChanges();

            return Get(menuId);
        }

        public MenuDto GetByName(string name)
        {
            var menu = _unitOfWork.MenuRepository
                .GetAll()
                .FirstOrDefault(x => name.Equals(x.Name));
            return menu is null ? null : _mapper.Map<MenuDto>(menu);
        }

        public void Delete(int id)
        {
            var menuDishes = _unitOfWork.MenuDishRepository.GetAll()
                .Where(x => x.MenuId == id);

            foreach (var item in menuDishes)
            {
                _unitOfWork.MenuDishRepository.Delete(item);
            }

            var menu = _unitOfWork.MenuRepository.Get(id);
            _unitOfWork.MenuRepository.Delete(menu);
            _unitOfWork.SaveChanges();
        }

        public MenuDto RemoveDishFromMenu(int menuId, int dishId)
        {
            var menuDish = _unitOfWork.MenuDishRepository.Get(menuId, dishId);
            if (menuDish is null)
            {
                return null;
            }

            _unitOfWork.MenuDishRepository.Delete(menuDish);
            _unitOfWork.SaveChanges();
            return Get(menuId);
        }
    }
}
