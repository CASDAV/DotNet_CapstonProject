using LogiTrack.Application.DTOs.Orders;
using LogiTrack.Application.Features.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace LogiTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly CreateOrder _createOrder;
        private readonly DeleteOrder _deleteOrder;
        private readonly GetOrderById _getOrderById;
        private readonly GetOrders _getOrders;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(CreateOrder createOrder, DeleteOrder deleteOrder, GetOrderById getOrderById, GetOrders getOrders, IMemoryCache memoryCache, ILogger<OrdersController> logger)
        {
            _createOrder = createOrder;
            _deleteOrder = deleteOrder;
            _getOrderById = getOrderById;
            _getOrders = getOrders;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            if (!_memoryCache.TryGetValue("Orders", out List<SimpleOrderDTO>? ordersCached))
            {
                _logger.LogInformation("Cache miss for key 'Orders' — loading from data store");
                var orders = await _getOrders.ExecuteAsync();

                _memoryCache.Set("Orders", orders, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                    SlidingExpiration = TimeSpan.FromSeconds(30),
                    Size = 1
                });
                _logger.LogInformation("Stored orders in cache under key 'orders'");
                return Ok(orders);

            }
            else
            {
                _logger.LogInformation($"Cache hit for key 'Orders' — returning orders");
                return Ok(ordersCached);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _getOrderById.ExecuteAsync(id);

            if (result == null) return NotFound($"the order with {id} can not be found");

            return Ok(result);

        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO order)
        {
            if (_memoryCache.TryGetValue("Orders", out _))
            {
                _memoryCache.Remove("Orders");
                _logger.LogInformation($"Removed cache entry 'Orders' due to creation of a new order");
            }

            return Ok(await _createOrder.ExecuteAsync(order));

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_memoryCache.TryGetValue("Orders", out _))
            {
                _memoryCache.Remove("Orders");
                _logger.LogInformation($"Removed cache entry 'Orders' due to deletion of order {id}");
            }

            return Ok(await _deleteOrder.ExecuteAsync(id));
        }
    }
}