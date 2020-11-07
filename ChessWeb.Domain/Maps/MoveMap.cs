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
            builder.HasOne(typeof(Game)).WithMany().HasForeignKey(nameof(Move.GameId));
            builder.HasOne(typeof(Player)).WithMany().HasForeignKey(nameof(Move.PlayerId));
            builder.Property(e => e.Fen).HasMaxLength(100);
            builder.Property(e => e.MoveNext).HasMaxLength(5);
        }
    }
}