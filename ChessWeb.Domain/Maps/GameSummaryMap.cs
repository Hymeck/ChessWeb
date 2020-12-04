using System.Collections.Generic;
using ChessWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChessWeb.Domain.Maps
{
    public class GameSummaryMap : IEntityBuilder<GameSummary>
    {
        public GameSummaryMap(EntityTypeBuilder<GameSummary> builder) => BuildEntity(builder);

        public void BuildEntity(EntityTypeBuilder<GameSummary> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Fen).HasMaxLength(100);
            builder.HasOne(e => e.Status).WithMany();
            builder.HasOne(e => e.WhiteUser).WithMany();
            builder.HasOne(e => e.BlackUser).WithMany();
            builder.Property(e => e.LastMove).HasMaxLength(5);
            builder.HasOne(e => e.ActiveColor).WithMany();
            builder.HasOne(e => e.Winner).WithMany();
        }
    }
}