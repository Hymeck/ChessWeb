using ChessWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChessWeb.Domain.Maps
{
    public class ColorMap : IEntityBuilder<Color>
    {
        public ColorMap(EntityTypeBuilder<Color> builder) => BuildEntity(builder);

        public void BuildEntity(EntityTypeBuilder<Color> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.ColorType);
            builder.HasIndex(e => e.ColorType).IsUnique();
            builder.HasData(new {Id = 1L, ColorType = true}, new {Id = 2L, ColorType = false});
        }
    }
}