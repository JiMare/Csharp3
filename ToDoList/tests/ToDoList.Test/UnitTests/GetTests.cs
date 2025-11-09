namespace ToDoList.Test;

using ToDoList.WebApi;
using ToDoList.Domain.DTOs;

using NSubstitute;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;


public class GetTests
{
    [Fact]
    public void Get_AllItems_ReturnsAllItems()
    {

        //Arrange
        var todoItem1 = new ToDoItemCreateRequestDto
        (
            Name: "Jmeno1",
            Description: "Popis1",
            IsCompleted: false
        );

        var todoItem2 = new ToDoItemCreateRequestDto
        (
            Name: "Jmeno2",
            Description: "Popis2",
            IsCompleted: false
        );

        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        controller.Create(todoItem1);
        controller.Create(todoItem2);
        //Act
        var result = controller.Read();
        var value = result.GetValue();
        //Assert
        Assert.NotNull(value);

        var firstToDo = value.First();
        Assert.Equal(todoItem1.Name, firstToDo.Name);
        Assert.Equal(todoItem1.Description, firstToDo.Description);
        Assert.Equal(todoItem1.IsCompleted, firstToDo.IsCompleted);

    }
}
