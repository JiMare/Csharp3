namespace ToDoList.Test;

using ToDoList.WebApi;
using ToDoList.Domain.DTOs;

using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;

public class DeleteTests
{
    [Fact]
    public void Delete_ExistingItem_ReturnsNoContent()
    {
        //Arrange
        var todoItem1 = new ToDoItemCreateRequestDto
        (
            Name: "Jmeno1",
            Description: "Popis1",
            IsCompleted: false
        );
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var item = controller.Create(todoItem1);
        //Act
        var result = controller.DeleteById(item.ToDoItemId);
        //Assert
        var noContent = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, noContent.StatusCode);
    }

    [Fact]
    public void Delete_MissingItem_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        // Act
        var result = controller.DeleteById(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
