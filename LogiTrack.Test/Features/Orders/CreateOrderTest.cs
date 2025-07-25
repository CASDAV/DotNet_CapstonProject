using AutoMapper;
using LogiTrack.Application.DTOs.Orders;
using LogiTrack.Application.Features.Orders;
using LogiTrack.Application.Interfaces.BusinessRepositories;
using LogiTrack.Domain.Entities.BusinessObjects;
using LogiTrack.Test.SetUp;
using Moq;

namespace LogiTrack.Test.Features.Orders
{
    public class CreateOrderTest
    {
        private readonly Mock<IOrdersRepository> _repoMock;
        private readonly CreateOrder _handler;
        private readonly IMapper _mapper;

        public CreateOrderTest()
        {
            _repoMock = new Mock<IOrdersRepository>();
            _mapper = MapperSetUp.SetUp();
            _handler = new CreateOrder(_repoMock.Object, _mapper);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldCallRepositoryAndReturnNewOrderId()
        {
            // Arrange
            var dto = new OrderCreateDTO
            {
                CustomerName = "Jhon Doe",
                DatePlaced = DateTime.Now
            };

            _repoMock
                .Setup(r => r.AddOrderAsync(It.IsAny<Order>()))
                .ReturnsAsync(42);

            // Act
            int result = await _handler.ExecuteAsync(dto);

            // Assert
            Assert.Equal(42, result);
            _repoMock.Verify(r => r.AddOrderAsync(It.IsAny<Order>()), Times.Once);
        }
    }
}
