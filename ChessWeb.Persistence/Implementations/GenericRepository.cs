using System.Threading.Tasks;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ChessWeb.Persistence.Implementations
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _dbContext;

        protected GenericRepository(ApplicationDbContext dbContext) => 
            _dbContext = dbContext;

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return entity;
        }

        public async Task<T> FindAsync(long id) =>
            await _dbContext.Set<T>().FindAsync(id);

        public async Task<int> SaveChangesAsync() =>
            await _dbContext.SaveChangesAsync();
    }
}