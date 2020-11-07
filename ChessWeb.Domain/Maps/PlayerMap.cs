﻿using ChessWeb.Domain.Entities;
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
            builder.HasData
            ( 
                new {Id = 1L, Email = "noonimf@gmail.com", Nickname = "Hymeck", Password = "hymeckpass"},
                new {Id = 2L, Email = "mr.yatson@gmail.com", Nickname = "Racoty", Password = "racotypass"},
                new {Id = 3L, Email = "vadimyaren@yandex.by", Nickname = "Yaren", Password = "yarenpass"},
                new {Id = 4L, Email = "some_email@gmail.com", Nickname = "Someone", Password = "someonepass"}
            );
        }
    }
}