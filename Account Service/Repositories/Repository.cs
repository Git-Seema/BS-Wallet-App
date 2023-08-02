using Microsoft.EntityFrameworkCore;
using AccountService.Context;

namespace AccountService.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AccountContext Context;
        private readonly DbSet<T> _entities;

        public Repository(AccountContext context)
        {
            Context = context;
            _entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _entities.Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _entities.FindAsync(id);
            if (entity == null) return;
            _entities.Remove(entity);
            await Context.SaveChangesAsync();
        }
    }

}
