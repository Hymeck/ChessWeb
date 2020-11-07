using ChessWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChessWeb.Domain.Maps
{
    public interface IEntityBuilder<T> where T : BaseEntity
    {
        void BuildEntity(EntityTypeBuilder<T> builder);
    }
}