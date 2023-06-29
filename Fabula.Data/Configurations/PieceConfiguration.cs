namespace Fabula.Data.Configurations;

using Models;
using Seeding.Seeders;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PieceConfiguration : IEntityTypeConfiguration<Piece>
{
    public void Configure(EntityTypeBuilder<Piece> builder)
    {
        builder.HasOne(p => p.Author)
            .WithMany(a => a.WrittenPieces)
            .HasForeignKey(p => p.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Comments)
            .WithOne(c => c.Piece)
            .HasForeignKey(c => c.PieceId)
            .OnDelete(DeleteBehavior.Restrict);

        //builder.HasData(PieceSeeder.SeedPieces());
    }
}
