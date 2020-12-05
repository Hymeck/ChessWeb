using System.Collections.Generic;
using System.Threading.Tasks;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Persistence.Contexts;

namespace ChessWeb.Persistence.Implementations
{
    public class SideRepository : GenericRepository<Side>, ISideRepository
    {
        public SideRepository(ApplicationDbContext dbContext) : base(dbContext) {}
    }
}