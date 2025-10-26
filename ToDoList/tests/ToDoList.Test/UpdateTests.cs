
namespace ToDoList.Test;

using ToDoList.Domain.Models;
using ToDoList.WebApi;
using ToDoList.Domain.DTOs;

using Microsoft.AspNetCore.Mvc;

public class UpdateTests
{

    [Fact]
    public void Update_Item_Should_Make_Change()
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

        var dto = new ToDoItemUpdateRequestDto(
           Name: "Nove jmeno",
           Description: "Novy popis",
           IsCompleted: true
       );
        //Act
        var result = controller.UpdateById(todoItem1.ToDoItemId, dto);
        var value = result.GetValue();
        //Assert
        Assert.NotNull(value);
        Assert.Equal("Nove jmeno", value.Name);
        Assert.Equal("Novy popis", value.Description);
        Assert.True(value.IsCompleted);
    }

}
