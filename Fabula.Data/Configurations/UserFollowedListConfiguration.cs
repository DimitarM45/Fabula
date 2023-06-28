namespace Fabula.Data.Configurations;

using Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserFollowedListConfiguration : IEntityTypeConfiguration<UserFollowedList>
{
    public void Configure(EntityTypeBuilder<UserFollowedList> builder)
    {
        builder.HasKey(ul => new { ul.UserId, ul.ListId });

        builder.HasOne(ul => ul.User)
            .WithMany(u => u.FollowedLists)
            .HasForeignKey(ul => ul.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ul => ul.List)
            .WithMany(l => l.Followers)
            .HasForeignKey(ul => ul.ListId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
