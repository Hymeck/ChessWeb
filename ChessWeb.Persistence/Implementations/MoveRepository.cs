﻿using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Interfaces;
using ChessWeb.Persistence.Contexts;

namespace ChessWeb.Persistence.Implementations
{
    public class MoveRepository : GenericRepository<Move>, IMoveRepository
    {
        public MoveRepository(ApplicationContext context) : base(context)
        {
        }
    }
}