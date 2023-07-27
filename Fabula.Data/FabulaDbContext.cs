namespace Fabula.Data;

using Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using System.Reflection;

public class FabulaDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public FabulaDbContext(DbContextOptions<FabulaDbContext> options)
        : base(options)
    {
    }

    public DbSet<Composition>? Compositions { get; set; }

    public DbSet<Rating>? Ratings { get; set; }

    public DbSet<Genre>? Genres { get; set; }

    public DbSet<Comment>? Comments { get; set; }

    public DbSet<Tag>? Tags { get; set; }

    public DbSet<List>? Lists { get; set; }

    public DbSet<UserLikedComment>? UsersLikedComments { get; set; }

    public DbSet<UserFavoriteComposition>? UsersFavoriteCompositions { get; set; }

    public DbSet<UserFollowedList>? UsersFollowedLists { get; set; }

    public DbSet<UserLikedList>? UsersLikedLists { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}