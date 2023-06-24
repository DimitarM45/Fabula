namespace Fabula.Data.Configurations;

using Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UsersLikedCommentsConfiguration : IEntityTypeConfiguration<UsersLikedComments>
{
    public void Configure(EntityTypeBuilder<UsersLikedComments> builder)
    {
        builder.HasKey(uc => new { uc.UserId, uc.CommentId });

        builder.HasOne(uc => uc.User)
            .WithMany(u => u.LikedComments)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(uc => uc.Comment)
            .WithMany(c => c.Likes)
            .HasForeignKey(uc => uc.CommentId);
    }
}
