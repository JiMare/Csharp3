

namespace ToDoList.Persistence.Repositories;

using ToDoList.Domain.Models;

public class ToDoItemsRepository : IRepositoryAsync<ToDoItem>
{

    private readonly ToDoItemsContext context;
    public ToDoItemsRepository(ToDoItemsContext context)
    {
        this.context = context;

    }

    public async Task CreateAsync(ToDoItem item)
    {
        context.ToDoItems.Add(item);
        await context.SaveChangesAsync();
    }


    public async Task<ToDoItem?> ReadByIdAsync(int id)
    {
        return context.ToDoItems.FirstOrDefault(i => i.ToDoItemId == id);
    }

    public async Task<IEnumerable<ToDoItem>> ReadAsync()
    {
        return context.ToDoItems.ToList();
    }


    public async Task<ToDoItem> UpdateByIdAsync(int id, Action<ToDoItem> request)
    {

        var dbItem = await ReadByIdAsync(id);
        if (dbItem == null)
        {
            return null;
        }
        request(dbItem);
        await context.SaveChangesAsync();
        return dbItem;
    }

    public async Task<int> DeleteByIdAsync(int id)
    {
        var item = await ReadByIdAsync(id);
        if (item == null)
        {
            return 0;
        }
        context.ToDoItems.Remove(item);
        await context.SaveChangesAsync();
        return 1;
    }
}
