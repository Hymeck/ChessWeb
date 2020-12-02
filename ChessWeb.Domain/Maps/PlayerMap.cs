using ChessWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChessWeb.Domain.Maps
{
    public class PlayerMap : IEntityBuilder<User>
    {
        public PlayerMap(EntityTypeBuilder<User> builder) => BuildEntity(builder);

        public void BuildEntity(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(e => e.Email).IsUnique();
        }
    }
}