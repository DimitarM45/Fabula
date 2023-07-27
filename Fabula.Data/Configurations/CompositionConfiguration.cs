namespace Fabula.Data.Configurations;

using Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CompositionConfiguration : IEntityTypeConfiguration<Composition>
{
    public void Configure(EntityTypeBuilder<Composition> builder)
    {
        builder.HasOne(p => p.Author)
            .WithMany(a => a.WrittenCompositions)
            .HasForeignKey(p => p.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Comments)
            .WithOne(c => c.Composition)
            .HasForeignKey(c => c.CompositionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
