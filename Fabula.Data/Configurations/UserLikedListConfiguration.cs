namespace Fabula.Data.Configurations;

using Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class UserLikedListConfiguration : IEntityTypeConfiguration<UserLikedList>
{
    public void Configure(EntityTypeBuilder<UserLikedList> builder)
    {
        builder.HasKey(ul => new { ul.UserId, ul.ListId });

        builder.HasOne(ul => ul.User)
            .WithMany(u => u.LikedLists)
            .HasForeignKey(ul => ul.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ul => ul.List)
            .WithMany(l => l.Likes)
            .HasForeignKey(ul => ul.ListId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
