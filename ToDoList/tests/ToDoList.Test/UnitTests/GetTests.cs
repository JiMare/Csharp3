namespace ToDoList.Test;

using ToDoList.WebApi;
using ToDoList.Domain.DTOs;

using NSubstitute;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;
using Microsoft.AspNetCore.Mvc;

public class GetTests
{
    [Fact]
    public async Task Get_AllItems_ReturnsAllItems()
    {

        //Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var someItem = new ToDoItem { Name = "testName", Description = "testDescription", IsCompleted = false };
        repositoryMock.ReadAsync().Returns(new[] { someItem });
        //Act
        var result = await controller.ReadAsync();
        //Assert
        Assert.IsType<ActionResult<IEnumerable<ToDoItemGetResponseDto>>>(result);

        repositoryMock.Received(1).ReadAsync();

    }
}
