using System.Threading.Tasks;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ChessWeb.Persistence.Implementations
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext DbContext;

        protected GenericRepository(ApplicationDbContext dbContext) => 
            DbContext = dbContext;

        public async Task<T> AddAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            return entity;
        }

        public async Task<T> FindAsync(int id) =>
            await DbContext.Set<T>().FindAsync(id);

        public async Task<int> SaveChangesAsync() =>
            await DbContext.SaveChangesAsync();
    }
}