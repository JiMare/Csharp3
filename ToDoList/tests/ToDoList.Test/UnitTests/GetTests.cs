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
    public void Get_AllItems_ReturnsAllItems()
    {

        //Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var someItem = new ToDoItem { Name = "testName", Description = "testDescription", IsCompleted = false };
        repositoryMock.Read().Returns([someItem]);
        //Act
        var result = controller.Read();
        //Assert
        Assert.IsType<ActionResult<IEnumerable<ToDoItemGetResponseDto>>>(result);

        repositoryMock.Received(1).Read();

    }
}
