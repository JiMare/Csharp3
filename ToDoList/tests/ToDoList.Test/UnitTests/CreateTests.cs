
namespace ToDoList.Test;

using ToDoList.WebApi;
using ToDoList.Domain.DTOs;

using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;

public class CreateTests
{
    [Fact]
    public async Task Create_Item_Returns_204()
    {
        //Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var dto = new ToDoItemCreateRequestDto(
            Name: "Úkol",
            Description: "Popis",
            IsCompleted: false,
            Category: "Test category"
        );
        //Act
        var result = await controller.CreateAsync(dto);
        //Assert
        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(ToDoItemsController.ReadByIdAsync), created.ActionName);

        var body = Assert.IsType<ToDoItemGetResponseDto>(created.Value);
        Assert.Equal("Úkol", body.Name);
        Assert.Equal("Popis", body.Description);
        Assert.Equal("Test category", body.Category);
        Assert.False(body.IsCompleted);
    }
}
