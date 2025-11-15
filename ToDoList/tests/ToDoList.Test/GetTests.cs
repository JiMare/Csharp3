namespace ToDoList.Test;

using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.WebApi;
public class GetTests
{
    [Fact]
    public void Get_AllItems_ReturnsAllItems()
    {

        //Arrange
        var todoItem1 = new ToDoItem
        {
            Name = "Jmeno1",
            Description = "Popis1",
            IsCompleted = false
        };

        var todoItem2 = new ToDoItem
        {
            Name = "Jmeno2",
            Description = "Popis2",
            IsCompleted = false
        };
        var connectionString = "Data Source=../../../../../data/localdb_test.db";
        using var context = new ToDoItemsContext(connectionString);
        var controller = new ToDoItemsController(context: context);
        controller.AddItemToStorage(todoItem1);
        controller.AddItemToStorage(todoItem2);
        //Act
        var result = controller.Read();
        var value = result.GetValue();
        //Assert
        Assert.NotNull(value);

        Assert.Contains(value, x =>
     x.Name == todoItem1.Name &&
     x.Description == todoItem1.Description &&
     x.IsCompleted == todoItem1.IsCompleted
 );
        Assert.Contains(value, x =>
            x.Name == todoItem2.Name &&
            x.Description == todoItem2.Description &&
            x.IsCompleted == todoItem2.IsCompleted
        );

    }
}
