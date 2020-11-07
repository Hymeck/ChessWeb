﻿// <auto-generated />
using System;
using ChessWeb.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ChessWeb.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0-rc.2.20475.6");

            modelBuilder.Entity("ChessWeb.Domain.Entities.Color", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<bool>("ColorType")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Color");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ColorType = true
                        },
                        new
                        {
                            Id = 2L,
                            ColorType = false
                        });
                });

            modelBuilder.Entity("ChessWeb.Domain.Entities.Game", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Fen")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Game");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"
                        });
                });

            modelBuilder.Entity("ChessWeb.Domain.Entities.Move", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Fen")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long?>("GameId")
                        .HasColumnType("bigint");

                    b.Property<string>("MoveNext")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<long?>("PlayerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Move");
                });

            modelBuilder.Entity("ChessWeb.Domain.Entities.Player", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nickname")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Nickname")
                        .IsUnique()
                        .HasFilter("[Nickname] IS NOT NULL");

                    b.ToTable("Player");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Email = "noonimf@gmail.com",
                            Nickname = "Hymeck",
                            Password = "hymeckpass"
                        },
                        new
                        {
                            Id = 2L,
                            Email = "mr.yatson@gmail.com",
                            Nickname = "Racoty",
                            Password = "racotypass"
                        },
                        new
                        {
                            Id = 3L,
                            Email = "vadimyaren@yandex.by",
                            Nickname = "Yaren",
                            Password = "yarenpass"
                        },
                        new
                        {
                            Id = 4L,
                            Email = "some_email@gmail.com",
                            Nickname = "Someone",
                            Password = "someonepass"
                        });
                });

            modelBuilder.Entity("ChessWeb.Domain.Entities.Side", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long?>("ColorId")
                        .HasColumnType("bigint");

                    b.Property<long?>("GameId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PlayerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.HasIndex("GameId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Side");
                });

            modelBuilder.Entity("ChessWeb.Domain.Entities.Move", b =>
                {
                    b.HasOne("ChessWeb.Domain.Entities.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId");

                    b.HasOne("ChessWeb.Domain.Entities.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");

                    b.Navigation("Game");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("ChessWeb.Domain.Entities.Side", b =>
                {
                    b.HasOne("ChessWeb.Domain.Entities.Color", "Color")
                        .WithMany()
                        .HasForeignKey("ColorId");

                    b.HasOne("ChessWeb.Domain.Entities.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId");

                    b.HasOne("ChessWeb.Domain.Entities.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");

                    b.Navigation("Color");

                    b.Navigation("Game");

                    b.Navigation("Player");
                });
#pragma warning restore 612, 618
        }
    }
}
