using ChessWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChessWeb.Domain.Maps
{
    public class PlayerMap
    {
        public PlayerMap(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Email).IsRequired();
            builder.Property(e => e.Password).IsRequired();
            builder.HasIndex(e => e.Nickname).IsUnique();
        }
    }
}