using AutoMapper;
using LogiTrack.Application.DTOs.Orders;
using LogiTrack.Application.Features.Orders;
using LogiTrack.Application.Interfaces.BusinessRepositories;
using LogiTrack.Domain.Entities.BusinessObjects;
using LogiTrack.Test.SetUp;
using Moq;
using Xunit;
using System.Threading.Tasks;

namespace LogiTrack.Test.Features.Orders;

public class GetOrderByIdTest
{
    private readonly Mock<IOrdersRepository> _repoMock;
    private readonly GetOrderById _handler;
    private readonly IMapper _mapper;

    public GetOrderByIdTest()
    {
        _repoMock = new Mock<IOrdersRepository>();
        _mapper = MapperSetUp.SetUp(); // ensures profile includes mapping Order -> OrderDetailsDTO
        _handler = new GetOrderById(_repoMock.Object, _mapper);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnDTO_WhenOrderExists()
    {
        // Arrange
        var order = new Order
        {
            Id = 5,
            CustomerName = "Jhon Doe",
            DatePlaced = DateTime.Now,
            // Add other required properties
        };
        _repoMock.Setup(r => r.GetOrderByIdAsync(5))
                 .ReturnsAsync(order);

        // Act
        var result = await _handler.ExecuteAsync(5);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Id);
        Assert.Equal("Jhon Doe", result.CustomerName);
        // Add asserts for other relevant mapped properties

        _repoMock.Verify(r => r.GetOrderByIdAsync(5), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnNull_WhenOrderDoesNotExist()
    {
        // Arrange
        _repoMock.Setup(r => r.GetOrderByIdAsync(It.IsAny<int>()))
                 .ReturnsAsync((Order?)null);

        // Act
        var result = await _handler.ExecuteAsync(999);

        // Assert
        Assert.Null(result);
        _repoMock.Verify(r => r.GetOrderByIdAsync(999), Times.Once);
    }
}
