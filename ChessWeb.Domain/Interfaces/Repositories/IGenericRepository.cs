using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChessWeb.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        T Get(long id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}