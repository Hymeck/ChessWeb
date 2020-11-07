using ChessWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChessWeb.Domain.Maps
{
    public class MoveMap : IEntityBuilder<Move>
    {
        public MoveMap(EntityTypeBuilder<Move> builder) => BuildEntity(builder);
        public void BuildEntity(EntityTypeBuilder<Move> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Game).WithMany().HasForeignKey("GameId");
            builder.HasOne(e => e.Player).WithMany().HasForeignKey("PlayerId");
            builder.Property(e => e.Fen).HasMaxLength(100);
            builder.Property(e => e.MoveNext).HasMaxLength(5);
        }
    }
}