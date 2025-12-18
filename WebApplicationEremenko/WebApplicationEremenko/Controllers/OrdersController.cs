using Microsoft.AspNetCore.Mvc;
using WebApplicationEremenko.Core.DTO;
using WebApplicationEremenko.Core.Services;

namespace WebApplicationEremenko.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null)
                return NotFound(new { Message = $"Заказ с ID {id} не найден" });

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderDto createOrderDto)
        {
            try
            {
                var createdOrder = await _orderService.CreateOrderAsync(createOrderDto);
                return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, UpdateOrderStatusDto updateDto)
        {
            try
            {
                var success = await _orderService.UpdateOrderStatusAsync(id, updateDto);

                if (!success)
                    return NotFound(new { Message = $"Заказ с ID {id} не найден" });

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetCustomerOrders(int customerId)
        {
            var orders = await _orderService.GetCustomerOrdersAsync(customerId);
            return Ok(orders);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByStatus(string status)
        {
            var orders = await _orderService.GetOrdersByStatusAsync(status);
            return Ok(orders);
        }
    }
}
