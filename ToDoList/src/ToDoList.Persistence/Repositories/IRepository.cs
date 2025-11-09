

namespace ToDoList.Persistence.Repositories;

public interface IRepository<T>
where T : class
{
    public void Create(T item);

    public T? UpdateById(int id, Action<T> request);

    public T? ReadById(int id);

    public IEnumerable<T> Read();

    public void DeleteById(T item);

}
