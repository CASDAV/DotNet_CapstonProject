using LogiTrack.Application.DTOs.InventoryItems;
using LogiTrack.Application.Features.InventoryItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        public InventoryItemsController(CreateInventoryItem createInventoryItem, DeleteInventoryItem deleteInventoryItem, GetInventoryItemById getInventoryItemById, GetInventoryItems getInventoryItems, GetInventoryItemsByOrderId getInventoryItemsByOrderId)
        {
            _createInventoryItem = createInventoryItem;
            _deleteInventoryItem = deleteInventoryItem;
            _getInventoryItemById = getInventoryItemById;
            _getInventoryItems = getInventoryItems;
            _getInventoryItemsByOrderId = getInventoryItemsByOrderId;
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateInventoryItem([FromBody] CreateInventoryItemDTO item)
            => Ok(await _createInventoryItem.ExecuteAsync(item));

        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteInventoryItem(int id)
            => Ok(await _deleteInventoryItem.ExecuteAsync(id));

        [HttpGet("{id}")]

        public async Task<IActionResult> GetInventoryItemById(int id)
            => Ok(await _getInventoryItemById.ExecuteAsync(id));

        [HttpGet]
        public async Task<IActionResult> GetAllInventoryItems()
            => Ok(await _getInventoryItems.ExecuteAsync());

        [HttpGet("byorder/{id}")]
        public async Task<IActionResult> GetInventoryItemsByOrderId(int id)
            => Ok(await _getInventoryItemsByOrderId.ExecuteAsync(id));
    }
}
