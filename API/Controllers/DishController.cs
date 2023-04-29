using API.ViewModels;
using AutoMapper;
using BLL.Dtos;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDishService _dishService;
        private readonly IDishIngredientService _dishIngredientService;
        private readonly IIngredientService _ingredientService;

        public DishController(IDishService dishService, IMapper mapper, IDishIngredientService dishIngredientService, IIngredientService ingredientService)
        {
            _dishService = dishService;
            _mapper = mapper;
            _dishIngredientService = dishIngredientService;
            _ingredientService = ingredientService;
        }

        [HttpGet]
        public IEnumerable<DishViewModel> GetAll()
        {
            var dishes = _dishService.GetAll().ToList();
            return _mapper.Map<List<DishViewModel>>(dishes);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            var dishDto = _dishService.Get(id);
            if (dishDto is null)
            {
                return NotFound($"The Dish with {id} Id is not found");
            }

            var dishViewModel = _mapper.Map<DishWithIngredientsViewModel>(dishDto);
            var dishIngredients = _dishIngredientService.GetByDish(id);
            dishViewModel.IngredientsList = dishIngredients.ToList();
            return Ok(dishViewModel);
        }

        [HttpGet]
        [Route("GetByName/{name}")]
        public IActionResult GetByName(string name)
        {
            var dishes = _dishService.GetByNameAll(name);
            if (dishes is null)
            {
                return NotFound($"The Dish with {name} name is not found");
            }

            var dishViewModels = _mapper.Map<IEnumerable<DishWithIngredientsViewModel>>(dishes);
            foreach (var vm in dishViewModels)
            {
                var dishIngredients = _dishIngredientService.GetByDish(vm.Id);
                vm.IngredientsList = dishIngredients.ToList();
            }

            return Ok(dishViewModels);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult CreateDish([FromBody] CreateDishDto createDishDto)
        {
            var existingDish = _dishService.GetByName(createDishDto.Name);
            if (existingDish is not null)
            {
                return Ok(existingDish);
            }

            var createdDish = _dishService.Create(createDishDto);
            return Created("Create", createdDish);
        }

        [HttpPost]
        [Route("{dishId:int}/AddIngredient/{ingredientId:int}")]
        public IActionResult AddIngredientToDish(int dishId, int ingredientId)
        {
            var dish = _dishService.Get(dishId);
            if (dish is null)
            {
                return NotFound($"The Dish with {dishId} Id is not found");
            }

            var ingredient = _ingredientService.Get(ingredientId);
            if (ingredient is null)
            {
                return NotFound($"The Ingredient with {ingredientId} Id is not found");
            }

            var updatedDish = _dishService.AddIngredientToDish(dishId, ingredientId);

            var dishViewModel = _mapper.Map<DishWithIngredientsViewModel>(updatedDish);
            var dishIngredients = _dishIngredientService.GetByDish(dishId);
            dishViewModel.IngredientsList = dishIngredients.ToList();
            return Ok(dishViewModel);
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var dish = _dishService.Get(id);
            if (dish is null)
            {
                return NotFound($"The Dish with {id} Id is not found");
            }

            _dishService.Delete(id);
            return NoContent();
        }

        [HttpDelete]
        [Route("Delete/{dishIdid:int}/Ingredient/{ingredientId:int}")]
        public IActionResult DeleteIngredientFromDish(int dishId, int ingredientId)
        {
            var dish = _dishService.Get(dishId);
            if (dish is null)
            {
                return NotFound($"The Dish with {dishId} Id is not found");
            }

            var ingredient = _ingredientService.Get(ingredientId);
            if (ingredient is null)
            {
                return NotFound($"The Ingredient with {ingredientId} Id is not found");
            }

            var updatedDish = _dishService.DeleteIngredientFromDish(dishId, ingredientId);
            if (updatedDish is null)
            {
                return NotFound($"There`s no ingredient with {ingredientId} id in the dish with {dishId} id found");
            }

            var dishViewModel = _mapper.Map<DishWithIngredientsViewModel>(updatedDish);
            var dishIngredients = _dishIngredientService.GetByDish(dishId);
            dishViewModel.IngredientsList = dishIngredients.ToList();
            return Ok(dishViewModel);
        }
    }
}
