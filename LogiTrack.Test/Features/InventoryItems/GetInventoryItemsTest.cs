using AutoMapper;
using LogiTrack.Application.DTOs.InventoryItems;
using LogiTrack.Application.Features.InventoryItems;
using LogiTrack.Application.Interfaces.BusinessRepositories;
using LogiTrack.Domain.Entities.BusinessObjects;
using LogiTrack.Test.SetUp;
using Moq;

namespace LogiTrack.Test.Features.InventoryItems;

public class GetInventoryItemsTest
{
    private readonly Mock<IInventoryItemRepository> _repoMock;
    private readonly GetInventoryItems _processor;
    private readonly IMapper _mapper;

    public GetInventoryItemsTest()
    {
        _repoMock = new Mock<IInventoryItemRepository>();
        _mapper = MapperSetUp.SetUp();
        _processor = new GetInventoryItems(_repoMock.Object, _mapper);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnMappedDTOs_WhenRepositoryReturnsEntities()
    {
        // Arrange
        var entities = new List<InventoryItem>
        {
            new InventoryItem { Id = 1, Name = "Item A", Quantity = 5, Location = "Loc1" },
            new InventoryItem { Id = 2, Name = "Item B", Quantity = 10, Location = "Loc2" }
        };
        _repoMock.Setup(r => r.GetAllInventoryItemsAsync())
                 .ReturnsAsync(entities);

        // Act
        var result = await _processor.ExecuteAsync();

        // Assert: verify correct number and content
        var list = Assert.IsAssignableFrom<IEnumerable<SimpleInventoryItemDTO>>(result);
        var arr = new List<SimpleInventoryItemDTO>(list);
        Assert.Equal(2, arr.Count);

        Assert.Equal("Item A", arr[0].Name);
        Assert.Equal(5, arr[0].Quantity);

        Assert.Equal("Item B", arr[1].Name);
        Assert.Equal(10, arr[1].Quantity);

        _repoMock.Verify(r => r.GetAllInventoryItemsAsync(), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnEmptyList_WhenRepositoryReturnsEmpty()
    {
        // Arrange
        _repoMock.Setup(r => r.GetAllInventoryItemsAsync()).ReturnsAsync(new List<InventoryItem>());

        // Act
        var result = await _processor.ExecuteAsync();

        // Assert
        Assert.Empty(result);
        _repoMock.Verify(r => r.GetAllInventoryItemsAsync(), Times.Once);
    }
}
