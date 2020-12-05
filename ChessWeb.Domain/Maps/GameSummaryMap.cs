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
            builder.Property(e => e.Status);
            builder.Property(e => e.LastMove).HasMaxLength(5);
            builder.Property(e => e.ActiveColor);
            builder.Property(e => e.Winner).HasMaxLength(30);
        }
    }
}