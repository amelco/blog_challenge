using blog.Entities;
using blog.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace blog.Repositories
{
    public class BasicRepository<T> : IBasicRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BasicRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public async Task<T> Create(T item)
        {
            if (item == null) return null!;
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task Delete(int id)
        {
            var existing = await _dbSet.FindAsync(id);
            if (existing == null) return;
            _dbSet.Remove(existing);
            await _context.SaveChangesAsync();
        }

        public async Task<T> Update(int id, T item)
        {
            if (item == null) return null!;

            IQueryable<T> query = _dbSet;
            if (typeof(T) == typeof(BlogPost))
            {
                query = query.Include(nameof(BlogPost.Comments));
            }

            var existing = await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
            if (existing == null) return null!;

            _context.Entry(existing).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<List<T>?> Get()
        {
            IQueryable<T> query = _dbSet;
            if (typeof(T) == typeof(BlogPost))
            {
                query = query.Include(nameof(BlogPost.Comments));
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            IQueryable<T> query = _dbSet;
            if (typeof(T) == typeof(BlogPost))
            {
                query = query.Include(nameof(BlogPost.Comments));
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }
    }
}