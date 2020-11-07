using ChessWeb.Domain.Entities;
using ChessWeb.Persistence.Contexts;
using ChessWeb.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessWeb.Persistence.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity  
    {  
        private readonly ApplicationContext context;
        private DbSet<T> entities;

        public Repository(ApplicationContext context) =>
            (this.context, entities) = (context, context.Set<T>());
        
        public IEnumerable<T> GetAll() => 
            entities.AsEnumerable();

        public T Get(long id) => 
            entities.FirstOrDefault(s => s.Id == id);

        public void Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            
            entities.Add(entity);
            context.SaveChanges();
        }
  
        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entities.Update(entity);
            context.SaveChanges();  
        }
  
        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            
            entities.Remove(entity);  
            context.SaveChanges();  
        }
        
        public void Remove(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entities.Remove(entity);
        }  
  
        public void SaveChanges() => 
            context.SaveChanges();
    }
}