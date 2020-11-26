using ChessWeb.Domain.Entities;
using System.Collections.Generic;

namespace ChessWeb.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(long id);
        void Insert(T entity);
        void Update(T entity);
        // void InsertOrUpdate(T entity);
        void Delete(T entity);
        void Remove(T entity);
        void SaveChanges();
    }
}