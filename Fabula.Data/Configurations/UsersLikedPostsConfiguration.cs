namespace Fabula.Data.Configurations;

using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UsersLikedPostsConfiguration : IEntityTypeConfiguration<UsersLikedPosts>
{
    public void Configure(EntityTypeBuilder<UsersLikedPosts> builder)
    {
        builder.HasKey(up => new { up.UserId, up.PostId });

        builder.HasOne(up => up.User)
            .WithMany(u => u.LikedPosts)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(up => up.Post)
            .WithMany(p => p.Likes)
            .HasForeignKey(up => up.PostId);
    }
}
