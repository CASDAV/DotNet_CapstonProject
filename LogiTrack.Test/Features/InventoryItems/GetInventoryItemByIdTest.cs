using AutoMapper;
using LogiTrack.Application.DTOs.InventoryItems;
using LogiTrack.Application.Features.InventoryItems;
using LogiTrack.Application.Interfaces.BusinessRepositories;
using LogiTrack.Domain.Entities.BusinessObjects;
using LogiTrack.Test.SetUp;
using Moq;

namespace LogiTrack.Test.Features.InventoryItems
{
    public class GetInventoryItemByIdTest
    {
        private readonly Mock<IInventoryItemRepository> _repoMock;
        private readonly GetInventoryItemById _handler;
        private readonly IMapper _mapper;

        public GetInventoryItemByIdTest()
        {
            _repoMock = new Mock<IInventoryItemRepository>();
            _mapper = MapperSetUp.SetUp();
            _handler = new GetInventoryItemById(_repoMock.Object, _mapper);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnDTO_WhenEntityExists()
        {
            // Arrange
            var entity = new InventoryItem
            {
                Id = 7,
                Name = "Widget",
                Quantity = 25,
                Location = "Warehouse X",
                OrderId = 22
            };
            _repoMock.Setup(r => r.GetInventoryItemByIdAsync(7))
                     .ReturnsAsync(entity);

            // Act
            DetailedInventoryItemDTO? result = await _handler.ExecuteAsync(7);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(7, result.Id);
            Assert.Equal("Widget", result.Name);
            Assert.Equal(25, result.Quantity);
            Assert.Equal("Warehouse X", result.Location);
            _repoMock.Verify(r => r.GetInventoryItemByIdAsync(7), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnNull_WhenEntityNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.GetInventoryItemByIdAsync(It.IsAny<int>()))
                     .ReturnsAsync((InventoryItem?)null);

            // Act
            var result = await _handler.ExecuteAsync(999);

            // Assert
            Assert.Null(result);
            _repoMock.Verify(r => r.GetInventoryItemByIdAsync(999), Times.Once);
        }
    }
}
