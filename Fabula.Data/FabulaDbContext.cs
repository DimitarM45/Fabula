namespace Fabula.Data;

using Models;
using Configurations;

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

    public DbSet<Post>? Posts { get; set; }

    public DbSet<Comment>? Comments { get; set; }

    public DbSet<UsersLikedStories>? UsersLikedStories { get; set; }

    public DbSet<UsersLikedPosts>? UsersLikedPosts { get; set; }

    public DbSet<UsersLikedComments>? UsersLikedComments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationUserConfiguration());

        builder.ApplyConfiguration(new UsersLikedStoriesConfiguration());

        builder.ApplyConfiguration(new UsersLikedPostsConfiguration());

        builder.ApplyConfiguration(new UsersLikedCommentsConfiguration());
    }
}