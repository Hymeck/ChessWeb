using ChessWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChessWeb.Domain.Maps
{
    public class PlayerMap : IEntityBuilder<Player>
    {
        public PlayerMap(EntityTypeBuilder<Player> builder) => BuildEntity(builder);

        public void BuildEntity(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Email).IsRequired();
            builder.Property(e => e.Password).IsRequired();
            builder.HasIndex(e => e.Nickname).IsUnique();
        }
    }
}