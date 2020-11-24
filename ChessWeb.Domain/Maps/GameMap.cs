using System.Collections.Generic;
using ChessWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChessWeb.Domain.Maps
{
    public class GameMap : IEntityBuilder<Game>
    {
        public GameMap(EntityTypeBuilder<Game> builder) => BuildEntity(builder);

        public void BuildEntity(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Fen).HasMaxLength(100);
            builder.HasOne(e => e.ChessGameInfo).WithMany().HasForeignKey("ChessGameInfoId");
            // var testGame = new Game
            // {
            //     Id = 1L, 
            //     Fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1",
            //     ChessGameInfo = new ChessGameInfo
            //     {
            //         Id = 1L,
            //         HasBlackKingMoved = false,
            //         BlackKingSquare = "e8",
            //         HasWhiteKingMoved = false,
            //         HasBlackKingsideRookMoved = false,
            //         HasBlackQueensideRookMoved = false,
            //         HasWhiteKingsideRookMoved = false,
            //         HasWhiteQueensideRookMoved = false,
            //         WhiteKingSquare = "e1"
            //     }
            // };
            builder.OwnsOne(e => e.ChessGameInfo).HasData(new List<Game>
            {
                new Game
                {
                    Id = 1L,
                    Fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1",
                    ChessGameInfo = new ChessGameInfo
                    {
                        Id = 1L,
                        HasBlackKingMoved = false,
                        BlackKingSquare = "e8",
                        HasWhiteKingMoved = false,
                        HasBlackKingsideRookMoved = false,
                        HasBlackQueensideRookMoved = false,
                        HasWhiteKingsideRookMoved = false,
                        HasWhiteQueensideRookMoved = false,
                        WhiteKingSquare = "e1"
                    }
                }
            });
        }
    }
}