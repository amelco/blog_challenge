namespace blog.Interfaces
{
    public interface IBasicRepository<T>
    {
        public Task<T> Create(T item);
        public Task Delete(int id);
        public Task<T> Update(int id, T item);
        public Task<List<T>?> Get();
        public Task<T?> GetById(int id);
    }
}