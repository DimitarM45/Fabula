namespace Fabula.Data.Configurations;

using Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserFavoriteGenreConfiguration : IEntityTypeConfiguration<UserFavoriteGenre>
{
    public void Configure(EntityTypeBuilder<UserFavoriteGenre> builder)
    {
        builder.HasKey(ug => new { ug.UserId, ug.GenreId });

        builder.HasOne(ug => ug.User)
            .WithMany(u => u.FavoriteGenres)
            .HasForeignKey(ug => ug.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ug => ug.Genre)
            .WithMany(g => g.Favorites)
            .HasForeignKey(ug => ug.GenreId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
