
namespace ToDoList.Test;

using ToDoList.WebApi;
using ToDoList.Domain.DTOs;

using Microsoft.AspNetCore.Mvc;

public class CreateTests
{
    [Fact]
    public void Create_Item_Returns_204()
    {
        //Arrange
        var controller = new ToDoItemsController();
        var dto = new ToDoItemCreateRequestDto(
            Name: "Úkol",
            Description: "Popis",
            IsCompleted: false
        );
        //Act
        var result = controller.Create(dto);
        //Assert
        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(ToDoItemsController.ReadById), created.ActionName);

        var body = Assert.IsType<ToDoItemGetResponseDto>(created.Value);
        Assert.Equal("Úkol", body.Name);
        Assert.Equal("Popis", body.Description);
        Assert.False(body.IsCompleted);
    }
}
