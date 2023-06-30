namespace Fabula.Data.Configurations;

using Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserFavoritePieceConfiguration : IEntityTypeConfiguration<UserFavoriteComposition>
{
    public void Configure(EntityTypeBuilder<UserFavoriteComposition> builder)
    {
        builder.HasKey(up => new { up.UserId, up.CompositionId });

        builder.HasOne(up => up.User)
            .WithMany(u => u.FavoriteCompositions)
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(up => up.Composition)
            .WithMany(p => p.Favorites)
            .HasForeignKey(up => up.CompositionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
