using ChessWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChessWeb.Domain.Interfaces
{
    public interface IApplicationContext
    {
        public DbSet<Color> Colors { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Move> Moves { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Side> Sides { get; set; }
    }
}