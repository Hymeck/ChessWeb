using System;
using System.Collections.Generic;
using System.Linq;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces;
using ChessWeb.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ChessWeb.Persistence.Implementations
{
    public class MoveRepository: IRepository<Move>
    {
        private readonly ApplicationContext context;
        public readonly DbSet<Move> entities;

        public MoveRepository(ApplicationContext context) =>
            (this.context, entities) = (context, context.Set<Move>());
        
        public IEnumerable<Move> GetAll() => 
            entities.AsEnumerable();

        public Move Get(long id) => 
            entities
                .AsQueryable()
                .Include(e => e.Game)
                .Include(e => e.Player)
                .FirstOrDefault(s => s.Id == id);
        public void Insert(Move entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            
            entities.Add(entity);
            context.SaveChanges();
        }
  
        public void Update(Move entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entities.Update(entity);
            context.SaveChanges();  
        }

        public void Delete(Move entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            
            entities.Remove(entity);  
            context.SaveChanges();  
        }
        
        public void Remove(Move entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entities.Remove(entity);
        }  
  
        public void SaveChanges() => 
            context.SaveChanges();
    }
}