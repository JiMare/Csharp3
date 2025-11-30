namespace ToDoList.Test;

using ToDoList.WebApi;
using ToDoList.Domain.DTOs;

using NSubstitute;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;

public class UpdateTests
{

    [Fact]
    public async Task Update_Item_Should_Make_Change()
    {
        //Arrange
        var existingItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Původní jméno",
            Description = "Původní popis",
            IsCompleted = false
        };

        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        repositoryMock
            .UpdateByIdAsync(Arg.Is(1), Arg.Any<Action<ToDoItem>>())
            .Returns(callInfo =>
            {
                var updateAction = callInfo.Arg<Action<ToDoItem>>();
                updateAction(existingItem);
                return existingItem;
            });

        var controller = new ToDoItemsController(repositoryMock);

        var dto = new ToDoItemUpdateRequestDto(
            Name: "Nove jmeno",
            Description: "Novy popis",
            IsCompleted: true
        );
        //Act
        var result = await controller.UpdateByIdAsync(1, dto);
        var value = result.GetValue();
        //Assert
        Assert.NotNull(value);
        Assert.Equal("Nove jmeno", value.Name);
        Assert.Equal("Novy popis", value.Description);
        Assert.True(value.IsCompleted);

        repositoryMock.Received(1)
            .UpdateByIdAsync(1, Arg.Any<Action<ToDoItem>>());
    }

}

