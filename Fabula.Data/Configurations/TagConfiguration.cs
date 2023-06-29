namespace Fabula.Data.Configurations;

using Models;
using Seeding.Seeders;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
        => builder.HasData(TagSeeder.SeedTags());
}
