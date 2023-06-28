namespace Fabula.Data.Configurations;

using Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ListConfiguration : IEntityTypeConfiguration<List>
{
    public void Configure(EntityTypeBuilder<List> builder)
    {
        builder.HasMany(l => l.Likes)
            .WithOne(ul => ul.List)
            .HasForeignKey(ul => ul.ListId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
