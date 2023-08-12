namespace Fabula.Data.Configurations;

using Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CompositionGenreConfiguration : IEntityTypeConfiguration<CompositionGenre>
{
    public void Configure(EntityTypeBuilder<CompositionGenre> builder)
    {
        builder.HasKey(cg => new { cg.CompositionId, cg.GenreId });

        builder.HasOne(cg => cg.Composition)
            .WithMany(c => c.Genres)
            .HasForeignKey(cg => cg.CompositionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(cg => cg.Genre)
            .WithMany(g => g.Compositions)
            .HasForeignKey(cg => cg.GenreId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
