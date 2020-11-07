using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Maps;
using Microsoft.EntityFrameworkCore;

namespace ChessWeb.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        protected readonly string databaseName = "chess_db";
        
        public ApplicationContext() {}
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new PlayerMap(modelBuilder.Entity<Player>());
            new ColorMap(modelBuilder.Entity<Color>());
            new GameMap(modelBuilder.Entity<Game>());
            new MoveMap(modelBuilder.Entity<Move>());
            new SideMap(modelBuilder.Entity<Side>());
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database={databaseName};Trusted_Connection=True;");
        }
    }
}