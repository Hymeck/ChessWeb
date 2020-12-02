using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChessWeb.Persistence.Implementations
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationContext _context;

        public GenericRepository(ApplicationContext context) => 
            _context = context;

        public virtual T Get(long id) =>
            _context.Set<T>().Find(id);

        public virtual IEnumerable<T> GetAll() =>
            _context.Set<T>().ToList();

        public EntityEntry<T> Add(T entity) => 
            _context.Set<T>().Add(entity);

        public EntityEntry<T> Delete(T entity) =>
            _context.Set<T>().Remove(entity);

        public EntityEntry<T> Update(T entity) =>
            _context.Set<T>().Update(entity);
    }
}