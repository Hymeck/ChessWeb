using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChessWeb.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        T Get(long id);
        IEnumerable<T> GetAll();
        EntityEntry<T> Add(T entity);
        EntityEntry<T> Delete(T entity);
        EntityEntry<T> Update(T entity);
    }
}