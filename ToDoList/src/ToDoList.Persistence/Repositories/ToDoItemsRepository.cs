

namespace ToDoList.Persistence.Repositories;

using ToDoList.Domain.Models;

public class ToDoItemsRepository : IRepository<ToDoItem>
{

    private readonly ToDoItemsContext context;
    public ToDoItemsRepository(ToDoItemsContext context)
    {
        this.context = context;

    }

    public void Create(ToDoItem item)
    {
        context.ToDoItems.Add(item);
        context.SaveChanges();
    }


    public ToDoItem ReadById(int id)
    {
        return context.ToDoItems.FirstOrDefault(i => i.ToDoItemId == id);
    }

    public IEnumerable<ToDoItem> Read()
    {
        return context.ToDoItems.ToList();
    }

    public ToDoItem UpdateById(int id, Action<ToDoItem> request)
    {

        var dbItem = ReadById(id);
        if (dbItem == null)
        {
            return null;
        }
        request(dbItem);
        context.SaveChanges();
        return dbItem;
    }

    public void DeleteById(ToDoItem item)
    {
        context.ToDoItems.Remove(item);
        context.SaveChanges();
    }
}
