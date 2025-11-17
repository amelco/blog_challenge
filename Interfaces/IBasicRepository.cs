namespace blog.Interfaces
{
    public interface IBasicRepository<T>
    {
        Task<T> Create(T item);
        Task Delete(int id);
        Task<T> Update(int id, T item);
        Task<List<T>?> Get();
        Task<T?> GetById(int id);
    }
}