namespace Fabula.Data;

using Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class FabulaDbContext : IdentityDbContext<ApplicationUser>
{
    public FabulaDbContext(DbContextOptions<FabulaDbContext> options)
        : base(options)
    {
    }

    public DbSet<Story>? Stories { get; set; }

    public DbSet<Genre>? Genres { get; set; }

    public DbSet<ApplicationUser>? ApplicationUser { get; set; }

    public DbSet<UsersLikedStories>? UsersLikedStories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.HasMany(au => au.WrittenStories)
                .WithOne(s => s.Author)
                .HasForeignKey(s => s.AuthorId)
                .OnDelete(DeleteBehavior.ClientCascade);
        });

        builder.Entity<UsersLikedStories>(entity =>
        {
            entity.HasKey(us => new { us.UserId, us.StoryId });

            entity.HasOne(us => us.User)
                .WithMany(u => u.LikedStories)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(us => us.Story)
                .WithMany(s => s.Likes)
                .HasForeignKey(us => us.StoryId);
        });
    }
}