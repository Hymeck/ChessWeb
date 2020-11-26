using ChessWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChessWeb.Domain.Maps
{
    public class ChessGameInfoMap : IEntityBuilder<ChessGameInfo>
    {
        public ChessGameInfoMap(EntityTypeBuilder<ChessGameInfo> builder) => BuildEntity(builder);
        public void BuildEntity(EntityTypeBuilder<ChessGameInfo> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.HasWhiteKingMoved);
            builder.Property(e => e.HasBlackKingMoved);
            builder.Property(e => e.HasWhiteQueensideRookMoved);
            builder.Property(e => e.HasWhiteKingsideRookMoved);
            builder.Property(e => e.HasBlackQueensideRookMoved);
            builder.Property(e => e.HasBlackKingsideRookMoved);
            builder.Property(e => e.WhiteKingSquare).HasMaxLength(2);
            builder.Property(e => e.BlackKingSquare).HasMaxLength(2);
            // builder.Ignore(e => e.Game);
            // initial position information
            builder.HasData(new
            {
                Id = 1L,
                HasWhiteKingMoved = false,
                HasBlackKingMoved = false,
                HasWhiteQueensideRookMoved = false,
                HasWhiteKingsideRookMoved = false,
                HasBlackQueensideRookMoved = false,
                HasBlackKingsideRookMoved = false,
                WhiteKingSquare = "e1",
                BlackKingSquare = "e8"
            });
        }
    }
}