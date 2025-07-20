using LogiTrack.Application.DTOs.Orders;
using LogiTrack.Application.Features.Orders;
using Microsoft.AspNetCore.Mvc;

namespace LogiTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly CreateOrder _createOrder;

        private readonly DeleteOrder _deleteOrder;

        private readonly GetOrderById _getOrderById;

        private readonly GetOrders _getOrders;

        public OrdersController(CreateOrder createOrder, DeleteOrder deleteOrder, GetOrderById getOrderById, GetOrders getOrders)
        {
            _createOrder = createOrder;
            _deleteOrder = deleteOrder;
            _getOrderById = getOrderById;
            _getOrders = getOrders;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
            => Ok(await _getOrders.ExecuteAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
            => Ok(await _getOrderById.ExecuteAsync(id));

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO order)
            => Ok(await _createOrder.ExecuteAsync(order));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
            => Ok(await _deleteOrder.ExecuteAsync(id));
    }
}