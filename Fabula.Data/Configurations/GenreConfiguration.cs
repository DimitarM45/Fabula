namespace Fabula.Data.Configurations;

using Models;
using Seeding.Seeders;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
        => builder.HasData(GenreSeeder.SeedGenres());
}
