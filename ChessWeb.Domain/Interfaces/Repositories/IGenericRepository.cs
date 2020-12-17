using System.Threading.Tasks;
using ChessWeb.Domain.Entities;

namespace ChessWeb.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity);

        T Update(T entity);

        T Delete(T entity);
        
        Task<T> FindAsync(long id);
        Task<int> SaveChangesAsync();
    }
}