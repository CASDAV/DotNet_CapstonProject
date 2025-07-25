using LogiTrack.Application.Features.Orders;
using LogiTrack.Application.Interfaces.BusinessRepositories;
using Moq;

namespace LogiTrack.Test.Features.Orders
{
    public class DeleteOrderTest
    {
        private readonly Mock<IOrdersRepository> _ordersRepoMock;
        private readonly Mock<IInventoryItemRepository> _inventoryRepoMock;
        private readonly DeleteOrder _handler;

        public DeleteOrderTest()
        {
            _ordersRepoMock = new Mock<IOrdersRepository>();
            _inventoryRepoMock = new Mock<IInventoryItemRepository>();
            _handler = new DeleteOrder(_ordersRepoMock.Object, _inventoryRepoMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnTrue_WhenBothDeletesSucceed()
        {
            // Arrange
            int orderId = 100;
            _inventoryRepoMock
                .Setup(r => r.DeleteInventoryItemsByOrderId(orderId))
                .ReturnsAsync(true);
            _ordersRepoMock
                .Setup(r => r.DeleteOrderAsync(orderId))
                .ReturnsAsync(true);

            // Act
            bool result = await _handler.ExecuteAsync(orderId);

            // Assert
            Assert.True(result);
            _inventoryRepoMock.Verify(r => r.DeleteInventoryItemsByOrderId(orderId), Times.Once);
            _ordersRepoMock.Verify(r => r.DeleteOrderAsync(orderId), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnFalse_WhenInventoryDeletionFails()
        {
            // Arrange
            int orderId = 200;
            _inventoryRepoMock
                .Setup(r => r.DeleteInventoryItemsByOrderId(orderId))
                .ReturnsAsync(false);
            _ordersRepoMock
                .Setup(r => r.DeleteOrderAsync(orderId))
                .ReturnsAsync(true);

            // Act
            bool result = await _handler.ExecuteAsync(orderId);

            // Assert
            Assert.False(result);
            _inventoryRepoMock.Verify(r => r.DeleteInventoryItemsByOrderId(orderId), Times.Once);
            _ordersRepoMock.Verify(r => r.DeleteOrderAsync(orderId), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnFalse_WhenOrderDeletionFails()
        {
            // Arrange
            int orderId = 300;
            _inventoryRepoMock
                .Setup(r => r.DeleteInventoryItemsByOrderId(orderId))
                .ReturnsAsync(true);
            _ordersRepoMock
                .Setup(r => r.DeleteOrderAsync(orderId))
                .ReturnsAsync(false);

            // Act
            bool result = await _handler.ExecuteAsync(orderId);

            // Assert
            Assert.False(result);
            _inventoryRepoMock.Verify(r => r.DeleteInventoryItemsByOrderId(orderId), Times.Once);
            _ordersRepoMock.Verify(r => r.DeleteOrderAsync(orderId), Times.Once);
        }
    }
}
