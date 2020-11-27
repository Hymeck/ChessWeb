using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessWeb.Domain.Interfaces;
using ChessWeb.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ChessWeb.Persistence.Implementations
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationContext _context;

        public GenericRepository(ApplicationContext context) => 
            _context = context;

        public virtual async Task<T> Get(int id) =>
            await _context.Set<T>().FindAsync(id);

        public virtual IEnumerable<T> GetAll() =>
            _context.Set<T>().ToList();

        public async Task Add(T entity) => 
            await _context.Set<T>().AddAsync(entity);

        public void Delete(T entity) =>
            _context.Set<T>().Remove(entity);

        public void Update(T entity) =>
            _context.Set<T>().Update(entity);
    }
}