namespace Fabula.Data.Configurations;

using Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UsersLikedStoriesConfiguration : IEntityTypeConfiguration<UsersLikedStories>
{
    public void Configure(EntityTypeBuilder<UsersLikedStories> builder)
    {
        builder.HasKey(us => new { us.UserId, us.StoryId });

        builder.HasOne(us => us.User)
            .WithMany(u => u.LikedStories)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(us => us.Story)
            .WithMany(s => s.Likes)
            .HasForeignKey(us => us.StoryId);
    }
}
