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
    public void Delete_ExistingItem_ReturnsNoContent()
    {
        //Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var id = 1;

        repositoryMock.DeleteById(id).Returns(1);
        //Act
        var result = controller.DeleteById(id);
        //Assert
        Assert.IsType<NoContentResult>(result);
        repositoryMock.Received(1).DeleteById(id);
    }

    [Fact]
    public void Delete_MissingItem_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        repositoryMock.DeleteById(999).Returns(0);
        // Act
        var result = controller.DeleteById(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
