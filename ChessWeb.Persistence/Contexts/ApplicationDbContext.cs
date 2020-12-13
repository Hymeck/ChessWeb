using System;
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
            // optionsBuilder
            //     // .UseLazyLoadingProxies()
            //     .UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database={databaseName};Trusted_Connection=True;");

            optionsBuilder
                .UseNpgsql(GetHerokuConnectionString());
        }
        
        private string GetHerokuConnectionString() 
        {
            // Get the connection string from the ENV variables
            // postgres://{user}:{password}@{hostname}:{port}/{database-name}
            // var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            var connectionUrl =
                "postgres://uikbdgoiuzaabs:f01c116697df4f7800d93686721c13c64e813cc8411de5f80af89b05d11f7135@ec2-54-246-67-245.eu-west-1.compute.amazonaws.com:5432/d29cpkqqe1m7i0";

            // parse the connection string
            var databaseUri = new Uri(connectionUrl);

            var db = databaseUri.LocalPath.TrimStart('/');
            var userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);

            return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
        }
    }
}