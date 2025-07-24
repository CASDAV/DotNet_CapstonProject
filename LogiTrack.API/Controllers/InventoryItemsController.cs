using LogiTrack.Application.DTOs.InventoryItems;
using LogiTrack.Application.Features.InventoryItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace LogiTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryItemsController : ControllerBase
    {
        private readonly CreateInventoryItem _createInventoryItem;
        private readonly DeleteInventoryItem _deleteInventoryItem;
        private readonly GetInventoryItemById _getInventoryItemById;
        private readonly GetInventoryItems _getInventoryItems;
        private readonly GetInventoryItemsByOrderId _getInventoryItemsByOrderId;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<InventoryItemsController> _logger;

        public InventoryItemsController(CreateInventoryItem createInventoryItem, DeleteInventoryItem deleteInventoryItem, GetInventoryItemById getInventoryItemById, GetInventoryItems getInventoryItems, GetInventoryItemsByOrderId getInventoryItemsByOrderId, IMemoryCache memoryCache, ILogger<InventoryItemsController> logger)
        {
            _createInventoryItem = createInventoryItem;
            _deleteInventoryItem = deleteInventoryItem;
            _getInventoryItemById = getInventoryItemById;
            _getInventoryItems = getInventoryItems;
            _getInventoryItemsByOrderId = getInventoryItemsByOrderId;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateInventoryItem([FromBody] CreateInventoryItemDTO item)
        {
            if (_memoryCache.TryGetValue("InventoryItems", out _))
            {
                _memoryCache.Remove("InventoryItems");
                _logger.LogInformation($"Removed cache entry 'InventoryItems' due to creation of a new item");
            }

            return Ok(await _createInventoryItem.ExecuteAsync(item));

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteInventoryItem(int id)
        {
            if (_memoryCache.TryGetValue("InventoryItems", out _))
            {
                _memoryCache.Remove("InventoryItems");
                _logger.LogInformation($"Removed cache entry 'InventoryItems' due to deletion of item {id}");
            }

            return Ok(await _deleteInventoryItem.ExecuteAsync(id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInventoryItemById(int id)
        {
            var result = await _getInventoryItemById.ExecuteAsync(id);

            if (result == null) return NotFound($"The inventory item with {id} can not be found");

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInventoryItems()
        {
            if (!_memoryCache.TryGetValue("InventoryItems", out List<SimpleInventoryItemDTO>? cachedItems))
            {
                _logger.LogInformation("Cache miss for key 'InventoryItems' — loading from data store");
                var items = await _getInventoryItems.ExecuteAsync();
                _memoryCache.Set("InventoryItems", items, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                    SlidingExpiration = TimeSpan.FromSeconds(30),
                    Size = 1
                });
                _logger.LogInformation("Stored items in cache under key 'InventoryItems'");
                return Ok(items);
            }
            else
            {
                _logger.LogInformation($"Cache hit for key 'InventoryItems' — returning items");
                return Ok(cachedItems);
            }
        }

        [HttpGet("byorder/{id}")]
        public async Task<IActionResult> GetInventoryItemsByOrderId(int id)
        {
            return Ok(await _getInventoryItemsByOrderId.ExecuteAsync(id));
        }
    }
}
