using blog.Interfaces;

namespace blog.Repositories
{
    public class BasicRepository<T> : IBasicRepository<T>
    {
        Task<T> IBasicRepository<T>.Create(T item)
        {
            throw new NotImplementedException();
        }

        Task<T> IBasicRepository<T>.Update(int id, T item)
        {
            throw new NotImplementedException();
        }

        Task<T?> IBasicRepository<T>.GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>?> Get()
        {
            throw new NotImplementedException();
        }
    }
}
