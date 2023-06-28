namespace Fabula.Data.Configurations;

using Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserFavoritePieceConfiguration : IEntityTypeConfiguration<UserFavoritePiece>
{
    public void Configure(EntityTypeBuilder<UserFavoritePiece> builder)
    {
        builder.HasKey(up => new { up.UserId, up.PieceId });

        builder.HasOne(up => up.User)
            .WithMany(u => u.FavoritePieces)
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(up => up.Piece)
            .WithMany(p => p.Favorites)
            .HasForeignKey(up => up.PieceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
