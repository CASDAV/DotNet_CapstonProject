using LogiTrack.Application.Features.InventoryItems;
using LogiTrack.Application.Interfaces.BusinessRepositories;
using Moq;

namespace LogiTrack.Test.Features.InventoryItems;

public class DeleteInventoryItemTest
{
    private readonly Mock<IInventoryItemRepository> _moqRepository;
    private readonly DeleteInventoryItem _deleteInventoryItem;


    public DeleteInventoryItemTest()
    {

        _moqRepository = new Mock<IInventoryItemRepository>();

        _deleteInventoryItem = new DeleteInventoryItem(_moqRepository.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldCallRepositoryAndReturnTrue()
    {
        _moqRepository
           .Setup(repo => repo.DeleteInventoryItemAsync(It.IsAny<int>()))
           .ReturnsAsync(true);

        // Act
        bool result = await _deleteInventoryItem.ExecuteAsync(5);

        // Assert
        Assert.True(result);
        _moqRepository.Verify(repo => repo.DeleteInventoryItemAsync(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldCallRepositoryAndReturnFalse()
    {
        // Arrange: Setup the mock to return false
        _moqRepository
            .Setup(repo => repo.DeleteInventoryItemAsync(It.IsAny<int>()))
            .ReturnsAsync(false);

        // Act
        bool result = await _deleteInventoryItem.ExecuteAsync(123);

        // Assert
        Assert.False(result);
        _moqRepository.Verify(repo => repo.DeleteInventoryItemAsync(It.IsAny<int>()), Times.Once);
    }
}
