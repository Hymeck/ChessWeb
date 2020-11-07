using ChessWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChessWeb.Domain.Maps
{
    public class SideMap : IEntityBuilder<Side>
    {
        public SideMap(EntityTypeBuilder<Side> builder) => BuildEntity(builder);
        public void BuildEntity(EntityTypeBuilder<Side> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(typeof(Game)).WithMany().HasForeignKey(nameof(Side.GameId));
            builder.HasOne(typeof(Player)).WithMany().HasForeignKey(nameof(Side.PlayerId));
            builder.HasOne(typeof(Color)).WithMany().HasForeignKey(nameof(Side.ColorId));
        }
    }
}