namespace ToDoList.Persistence.Repositories;

public interface IRepositoryAsync<T>
    where T : class
{
    public Task CreateAsync(T item);

    public Task<T?> UpdateByIdAsync(int id, Action<T> request);

    public Task<T?> ReadByIdAsync(int id);

    public Task<IEnumerable<T>> ReadAsync();

    public Task<int> DeleteByIdAsync(int id);
}
