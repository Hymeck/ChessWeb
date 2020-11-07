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
            builder.HasOne(e => e.Game).WithMany().HasForeignKey("GameId");
            builder.HasOne(e => e.Player).WithMany().HasForeignKey("PlayerId");
            builder.HasOne(e => e.Color).WithMany().HasForeignKey("ColorId");
        }
    }
}