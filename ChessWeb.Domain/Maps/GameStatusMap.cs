using System.Collections.Generic;
using ChessWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChessWeb.Domain.Maps
{
    public class GameStatusMap : IEntityBuilder<GameStatus>
    {
        public GameStatusMap(EntityTypeBuilder<GameStatus> builder) => BuildEntity(builder);

        public void BuildEntity(EntityTypeBuilder<GameStatus> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Status);
            builder.HasIndex(e => e.Status).IsUnique();

            builder.HasData(new List<GameStatus>(4)
            {
                new() {Id = 1L, Status = 0 },
                new () {Id = 2L, Status = 1 },
                new () {Id = 3L, Status = 2 },
                new () {Id = 4L, Status = 3 },
                new () {Id = 5L, Status = 4 },
                new () {Id = 6L, Status = 5 }
            });
        }
    }
}