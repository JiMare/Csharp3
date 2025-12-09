namespace ToDoList.Test;

using ToDoList.WebApi;
using ToDoList.Domain.DTOs;

using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;

public class DeleteTests
{
    [Fact]
    public async Task Delete_ExistingItem_ReturnsNoContent()
    {
        //Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var id = 1;

        repositoryMock.DeleteByIdAsync(id).Returns(Task.FromResult(1));
        //Act
        var result = await controller.DeleteByIdAsync(id);
        //Assert
        Assert.IsType<NoContentResult>(result);
        repositoryMock.Received(1).DeleteByIdAsync(id);
    }

    [Fact]
    public async Task Delete_MissingItem_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        repositoryMock.DeleteByIdAsync(999).Returns(0);
        // Act
        var result = await controller.DeleteByIdAsync(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
