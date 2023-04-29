using API.ViewModels;
using AutoMapper;
using BLL.Dtos;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;
        private readonly IDishService _dishService;

        public MenuController(IMenuService menuService, IMapper mapper, IDishService dishService)
        {
            _menuService = menuService;
            _mapper = mapper;
            _dishService = dishService;
        }

        [HttpGet]
        public IEnumerable<MenuDto> GetAll()
        {
            var menus = _menuService.GetAll();
            return menus;
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            var menu = _menuService.Get(id);
            if (menu is null)
            {
                return NotFound($"The menu with {id} Id is not found");
            }

            var menuViewModel = _mapper.Map<MenuDishesViewModel>(menu);
            var dishes = _dishService.GetDishesByMenu(id).ToList();
            var dishVms = _mapper.Map<List<DishViewModel>>(dishes);
            menuViewModel.Dishes = dishVms;
            return Ok(menuViewModel);
        }

        [HttpGet]
        [Route("GetByName/{name}")]
        public IActionResult GetByName(string name)
        {
            var menu = _menuService.GetByName(name);
            if (menu is null)
            {
                return NotFound($"The menu with name {name} is not found");
            }

            return Ok(menu);
        }

        [HttpPost]
        [Route("Create/{name}")]
        public IActionResult CreateNewMenu(string name)
        {
            var existingMenu = _menuService.GetByName(name);
            if (existingMenu is not null)
            {
                return Ok(existingMenu);
            }

            var menuDto = new MenuDto() {Name = name};
            var createdMenu = _menuService.CreateMenu(menuDto);
            return Created($"Create/{name}", createdMenu);
        }

        [HttpPost]
        [Route("{menuId:int}/Add/{dishId:int}")]
        public IActionResult AddDishToMenu(int menuId, int dishId)
        {
            var menuDto = _menuService.Get(menuId);
            if (menuDto is null)
            {
                return NotFound($"The menu with {menuId} is not found");
            }

            var dishDto = _dishService.Get(dishId);
            if (dishDto is null)
            {
                return NotFound($"The dish with {dishId} is not found");
            }

            var updatedMenu = _menuService.AddDishToMenu(menuId, dishId);

            var menuViewModel = _mapper.Map<MenuDishesViewModel>(updatedMenu);
            var dishes = _dishService.GetDishesByMenu(menuId).ToList();
            var dishVms = _mapper.Map<List<DishViewModel>>(dishes);
            menuViewModel.Dishes = dishVms;
            return Ok(menuViewModel);
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public IActionResult RemoveMenu(int id)
        {
            var menuDto = _menuService.Get(id);
            if (menuDto is null)
            {
                return NotFound($"The menu with {id} is not found");
            }

            _menuService.Delete(id);
            return NoContent();
        }

        [HttpDelete]
        [Route("Delete/{menuId:int}/Dish/{dishId:int}")]
        public IActionResult RemoveMenu(int menuId, int dishId)
        {
            var menuDto = _menuService.Get(menuId);
            if (menuDto is null)
            {
                return NotFound($"The menu with {menuId} is not found");
            }

            var dishDto = _dishService.Get(dishId);
            if (dishDto is null)
            {
                return NotFound($"The dish with {dishId} is not found");
            }

            var updatedMenu = _menuService.RemoveDishFromMenu(menuId, dishId);
            if (updatedMenu is null)
            {
                return NotFound($"There`s no dish with {dishId} id in the menu with {menuId} id found");
            }
            var menuViewModel = _mapper.Map<MenuDishesViewModel>(updatedMenu);
            var dishes = _dishService.GetDishesByMenu(menuId).ToList();
            var dishVms = _mapper.Map<List<DishViewModel>>(dishes);
            menuViewModel.Dishes = dishVms;
            return Ok(menuViewModel);
        }
    }
}
