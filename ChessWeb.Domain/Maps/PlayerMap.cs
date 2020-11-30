using ChessWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChessWeb.Domain.Maps
{
    public class PlayerMap : IEntityBuilder<Player>
    {
        public PlayerMap(EntityTypeBuilder<Player> builder) => BuildEntity(builder);

        public void BuildEntity(EntityTypeBuilder<Player> builder)
        {
            // builder.HasKey(e => e.Id);
            // builder.Property(e => e.Email).IsRequired();
            builder.HasIndex(e => e.Nickname).IsUnique();
            // builder.HasData
            // ( 
            //     new {Email = "noonimf@gmail.com", Nickname = "Hymeck", Password = "hymeckpass"},
            //     new {Email = "mr.yatson@gmail.com", Nickname = "Racoty", Password = "racotypass"},
            //     new {Email = "vadimyaren@yandex.by", Nickname = "Yaren", Password = "yarenpass"},
            //     new {Email = "some_email@gmail.com", Nickname = "Someone", Password = "someonepass"}
            // );
        }
    }
}