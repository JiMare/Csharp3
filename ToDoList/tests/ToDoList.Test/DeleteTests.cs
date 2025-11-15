namespace ToDoList.Test;

using ToDoList.Domain.Models;
using ToDoList.WebApi;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Persistence;

public class DeleteTests
{
    [Fact]
    public void Delete_ExistingItem_ReturnsNoContent()
    {
        //Arrange
        var todoItem1 = new ToDoItem
        {
            Name = "Jmeno1",
            Description = "Popis1",
            IsCompleted = false
        };
        var connectionString = "Data Source=../../../../../data/localdb_test.db";
        using var context = new ToDoItemsContext(connectionString);
        var controller = new ToDoItemsController(context: context);
        controller.AddItemToStorage(todoItem1);
        //Act
        var result = controller.DeleteById(todoItem1.ToDoItemId);
        //Assert
        var noContent = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, noContent.StatusCode);
    }

    [Fact]
    public void Delete_MissingItem_ReturnsNotFound()
    {
        // Arrange
        var connectionString = "Data Source=../../../../../data/localdb_test.db";
        using var context = new ToDoItemsContext(connectionString);
        var controller = new ToDoItemsController(context: context);

        // Act
        var result = controller.DeleteById(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
