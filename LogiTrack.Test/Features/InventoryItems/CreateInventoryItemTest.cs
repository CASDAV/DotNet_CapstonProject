using AutoMapper;
using LogiTrack.Application.DTOs.InventoryItems;
using LogiTrack.Application.Features.InventoryItems;
using LogiTrack.Application.Interfaces.BusinessRepositories;
using LogiTrack.Application.Mapper;
using LogiTrack.Domain.Entities.BusinessObjects;
using LogiTrack.Test.SetUp;
using Microsoft.Extensions.Logging;
using Moq;

namespace LogiTrack.Test.Features.InventoryItems;

public class CreateInventoryItemTest
{
    private readonly Mock<IInventoryItemRepository> _moqRepository;
    private readonly CreateInventoryItem _createInventoryItem;
    private readonly IMapper _mapper;

    public CreateInventoryItemTest()
    {

        _mapper = MapperSetUp.SetUp();

        _moqRepository = new Mock<IInventoryItemRepository>();

        _createInventoryItem = new CreateInventoryItem(_moqRepository.Object, _mapper);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldCallRepositoryAndReturnId()
    {
        var dto = new CreateInventoryItemDTO
        {
            Name = "Item",
            Location = "Warehouse A",
            Quantity = 10
        };

        _moqRepository.Setup(repo => repo.AddInventoryItemAsync(It.IsAny<InventoryItem>())).ReturnsAsync(1);

        var result = await _createInventoryItem.ExecuteAsync(dto);

        //Assert
        Assert.Equal(1, result);
        _moqRepository.Verify(repo => repo.AddInventoryItemAsync(It.IsAny<InventoryItem>()), Times.Once);
    }
}
