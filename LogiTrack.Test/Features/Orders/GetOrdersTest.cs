using AutoMapper;
using LogiTrack.Application.DTOs.Orders;
using LogiTrack.Application.Features.Orders;
using LogiTrack.Application.Interfaces.BusinessRepositories;
using LogiTrack.Domain.Entities.BusinessObjects;
using LogiTrack.Test.SetUp;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogiTrack.Test.Features.Orders
{
    public class GetOrdersTest
    {
        private readonly Mock<IOrdersRepository> _ordersRepoMock;
        private readonly GetOrders _handler;
        private readonly IMapper _mapper;

        public GetOrdersTest()
        {
            _ordersRepoMock = new Mock<IOrdersRepository>();
            _mapper = MapperSetUp.SetUp();
            _handler = new GetOrders(_ordersRepoMock.Object, _mapper);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnMappedDTOs_WhenRepositoryReturnsEntities()
        {
            // Arrange
            var entities = new List<Order>
            {
                new Order { Id = 1, CustomerName = "Jhon Doe" /*, other props*/ },
                new Order { Id = 2, CustomerName = "Jhon Doe" /*, ...*/ }
            };
            _ordersRepoMock
                .Setup(r => r.GetAllOrdersAsync())
                .ReturnsAsync(entities);

            // Act
            var result = await _handler.ExecuteAsync();

            // Assert
            var list = Assert.IsAssignableFrom<IEnumerable<SimpleOrderDTO>>(result);
            var arr = new List<SimpleOrderDTO>(list);

            Assert.Equal(2, arr.Count);
            Assert.Equal(1, arr[0].Id);
            Assert.Equal("Jhon Doe", arr[0].CustomerName);
            Assert.Equal(2, arr[1].Id);
            Assert.Equal("Jhon Doe", arr[1].CustomerName);

            _ordersRepoMock.Verify(r => r.GetAllOrdersAsync(), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnEmptyList_WhenRepositoryReturnsEmpty()
        {
            // Arrange
            _ordersRepoMock
                .Setup(r => r.GetAllOrdersAsync())
                .ReturnsAsync(new List<Order>());

            // Act
            var result = await _handler.ExecuteAsync();

            // Assert
            Assert.Empty(result);
            _ordersRepoMock.Verify(r => r.GetAllOrdersAsync(), Times.Once);
        }
    }
}
