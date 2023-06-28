namespace Fabula.Data.Configurations;

using Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserLikedCommentConfiguration : IEntityTypeConfiguration<UserLikedComment>
{
    public void Configure(EntityTypeBuilder<UserLikedComment> builder)
    {
        builder.HasKey(uc => new { uc.UserId, uc.CommentId });

        builder.HasOne(uc => uc.User)
            .WithMany(u => u.LikedComments)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(uc => uc.Comment)
            .WithMany(c => c.Likes)
            .HasForeignKey(uc => uc.CommentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
