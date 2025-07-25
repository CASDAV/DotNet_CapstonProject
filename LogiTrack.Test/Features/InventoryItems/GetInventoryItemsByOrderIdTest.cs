using AutoMapper;
using LogiTrack.Application.DTOs.InventoryItems;
using LogiTrack.Application.Features.InventoryItems;
using LogiTrack.Application.Interfaces.BusinessRepositories;
using LogiTrack.Domain.Entities.BusinessObjects;
using LogiTrack.Test.SetUp;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogiTrack.Test.Features.InventoryItems
{
    public class GetInventoryItemsByOrderIdTest
    {
        private readonly Mock<IInventoryItemRepository> _repoMock;
        private readonly GetInventoryItemsByOrderId _handler;
        private readonly IMapper _mapper;

        public GetInventoryItemsByOrderIdTest()
        {
            _repoMock = new Mock<IInventoryItemRepository>();
            _mapper = MapperSetUp.SetUp();
            _handler = new GetInventoryItemsByOrderId(_repoMock.Object, _mapper);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnMappedDTOs_WhenRepositoryReturnsEntities()
        {
            // Arrange
            var entities = new List<InventoryItem>
            {
                new InventoryItem { Id = 10, Name = "Item1", Quantity = 3, Location = "LocA", OrderId = 22},
                new InventoryItem { Id = 20, Name = "Item2", Quantity = 7, Location = "LocB" , OrderId = 22}
            };
            _repoMock
                .Setup(r => r.GetInventoryItemsByOrderIdAsync(5))
                .ReturnsAsync(entities);

            // Act
            var result = await _handler.ExecuteAsync(5);

            // Assert
            var list = Assert.IsAssignableFrom<IEnumerable<SimpleInventoryItemDTO>>(result);
            var arr = new List<SimpleInventoryItemDTO>(list);
            Assert.Equal(2, arr.Count);
            Assert.Equal("Item1", arr[0].Name);
            Assert.Equal(3, arr[0].Quantity);
            Assert.Equal("Item2", arr[1].Name);
            Assert.Equal(7, arr[1].Quantity);

            _repoMock.Verify(r => r.GetInventoryItemsByOrderIdAsync(5), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnEmptyList_WhenRepositoryReturnsEmpty()
        {
            // Arrange
            _repoMock
                .Setup(r => r.GetInventoryItemsByOrderIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<InventoryItem>());

            // Act
            var result = await _handler.ExecuteAsync(42);

            // Assert
            Assert.Empty(result);
            _repoMock.Verify(r => r.GetInventoryItemsByOrderIdAsync(42), Times.Once);
        }
    }
}
