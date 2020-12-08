using ChessWeb.Domain.Entities;
using ChessWeb.Domain.Maps;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChessWeb.Persistence.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<User, UserRole, string>
    {
        protected readonly string databaseName = "ChessWebDb";
        public DbSet<Color> Colors { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Move> Moves { get; set; }
        public DbSet<Side> Sides { get; set; }
        public DbSet<GameStatus> GameStatuses { get; set; }
        // public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public ApplicationDbContext()
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new ColorMap(modelBuilder.Entity<Color>());
            new GameStatusMap(modelBuilder.Entity<GameStatus>());

            #region User
            modelBuilder
                .Entity<User>()
                .HasMany(e => e.Games)
                .WithMany("Users");
            #endregion User
            
            #region Game

            modelBuilder
                .Entity<Game>()
                .HasOne(e => e.WhiteUser)
                .WithMany();
            
            modelBuilder
                .Entity<Game>()
                .HasOne(e => e.BlackUser)
                .WithMany();
            #endregion Game

            #region Move
            modelBuilder
                .Entity<Move>()
                .HasOne(e => e.Game)
                .WithMany()
                .HasForeignKey(e => e.GameId);
            
            modelBuilder
                .Entity<Move>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId);
            #endregion Move
            
            #region Side
            modelBuilder
                .Entity<Side>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId);
            modelBuilder
                .Entity<Side>()
                .HasOne(e => e.Color)
                .WithMany()
                .HasForeignKey(e => e.ColorId);
            #endregion Side
            
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                // .UseLazyLoadingProxies()
                .UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database={databaseName};Trusted_Connection=True;");
        }
    }
}