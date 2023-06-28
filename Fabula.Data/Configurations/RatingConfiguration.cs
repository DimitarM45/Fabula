namespace Fabula.Data.Configurations;

using Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasOne(r => r.User)
            .WithMany(u => u.Ratings)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Piece)
            .WithMany(p => p.Ratings)
            .HasForeignKey(r => r.PieceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
