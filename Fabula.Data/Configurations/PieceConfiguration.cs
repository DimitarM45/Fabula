namespace Fabula.Data.Configurations;

using Models;

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

        builder.HasOne(p => p.Form)
            .WithMany(f => f.Pieces)
            .HasForeignKey(p => p.FormId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Comments)
            .WithOne(c => c.Piece)
            .HasForeignKey(c => c.PieceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
