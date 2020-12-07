﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ChessWeb.Persistence.Implementations
{
    public class SideRepository : GenericRepository<Side>, ISideRepository
    {
        public SideRepository(ApplicationDbContext dbContext) : base(dbContext) {}
        public async Task<IEnumerable<Side>> GetGameSides(Game game)
        {
            var sides =  _dbContext.Sides.Where(x => x.Game == game).AsEnumerable();
            // foreach (var side in sides)
            // {
            //     _dbContext.Entry(side).Reference(e => e.Color).Load();
            //     _dbContext.Entry(side).Reference(e => e.User).Load();
            // }

            return sides;
        }
    }
}