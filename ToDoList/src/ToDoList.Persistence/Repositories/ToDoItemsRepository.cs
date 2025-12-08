

namespace ToDoList.Persistence.Repositories;

using ToDoList.Domain.Models;
using Microsoft.EntityFrameworkCore;

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
        return await context.ToDoItems.FindAsync(id);
    }

    public async Task<IEnumerable<ToDoItem>> ReadAsync()
    {
        return await context.ToDoItems.ToListAsync();
    }


    public async Task<ToDoItem> UpdateByIdAsync(int id, Domain.DTOs.ToDoItemUpdateRequestDto dto)
    {

        var dbItem = await ReadByIdAsync(id);
        if (dbItem == null)
        {
            return null;
        }
        dbItem.Name = dto.Name;
        dbItem.Description = dto.Description;
        dbItem.IsCompleted = dto.IsCompleted;
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
