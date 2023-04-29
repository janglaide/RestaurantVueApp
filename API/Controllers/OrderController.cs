using AutoMapper;
using BLL.Dtos;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IDishService _dishService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper, IDishService dishService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _dishService = dishService;
        }

        [HttpGet]
        public IEnumerable<OrderDto> GetAll()
        {
            return _orderService.GetAll();
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            var orderDto = _orderService.Get(id);

            if (orderDto is null)
                return NotFound();
            
            return Ok(orderDto);
        }

        [HttpPost]
        [Route("{orderId:int}/AddDish/{dishId:int}")]
        public IActionResult AddDishToOrder(int orderId, int dishId)
        {
            var dishDto = _dishService.Get(dishId);
            if (dishDto is null)
                return NotFound($"The dish with {dishId} Id is not found");

            var orderDto = _orderService.AddDish(orderId, dishId);
            return Created($"{orderDto.Id:int}/AddDish/{dishId:int}", orderDto);
        }

        [HttpDelete]
        [Route("Remove/{id:int}")]
        public IActionResult RemoveOrder(int id)
        {
            var orderDto = _orderService.Get(id);
            if (orderDto is null)
                return NotFound($"The order with {id} Id is not found");

            _orderService.Remove(id);
            return Ok();
        }

        [HttpDelete]
        [Route("{orderId:int}/RemoveDish/{dishId:int}")]
        public IActionResult RemoveDishFromOrder(int orderId, int dishId)
        {
            var dishDto = _dishService.Get(dishId);
            if (dishDto is null)
                return NotFound($"The dish with {dishId} Id is not found");

            var orderDto = _orderService.Get(orderId);
            if (orderDto is null)
                return NotFound($"The order with {orderId} Id is not found");

            var proceedOrderDto = _orderService.RemoveDish(orderId, dishId);
            if (proceedOrderDto is null)
            {
                return NoContent();
            }
            return Ok(orderDto);
        }
    }
}
