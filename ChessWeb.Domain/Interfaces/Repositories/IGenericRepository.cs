using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChessWeb.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);
        IEnumerable<T> GetAll();
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}