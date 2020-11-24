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
            builder.Property(e => e.BlackKingSquare).HasMaxLength(2);
            builder.Property(e => e.WhiteKingSquare).HasMaxLength(2);
            builder.Property(e => e.HasBlackKingMoved);
            builder.Property(e => e.HasWhiteKingMoved);
            builder.Property(e => e.HasBlackKingsideRookMoved);
            builder.Property(e => e.HasBlackQueensideRookMoved);
            builder.Property(e => e.HasWhiteKingsideRookMoved);
            builder.Property(e => e.HasWhiteQueensideRookMoved);
            // initial board info that does not contain in the FEN
            builder.HasData(new
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
            });
        }
    }
}