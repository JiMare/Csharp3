namespace ToDoList.Test;

using ToDoList.Domain.Models;
using ToDoList.WebApi;

using Microsoft.AspNetCore.Mvc;

public class DeleteTests
{
    [Fact]
    public void Delete_ExistingItem_ReturnsNoContent()
    {
        //Arrange
        var todoItem1 = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno1",
            Description = "Popis1",
            IsCompleted = false
        };
        var controller = new ToDoItemsController();
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
        var controller = new ToDoItemsController();

        // Act
        var result = controller.DeleteById(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
