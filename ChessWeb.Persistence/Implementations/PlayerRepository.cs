﻿using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces.Repositories;
using ChessWeb.Persistence.Contexts;

namespace ChessWeb.Persistence.Implementations
{
    public class PlayerRepository : GenericRepository<User>, IPlayerRepository
    {
        public PlayerRepository(ApplicationContext context) : base(context)
        {
        }
    }
}