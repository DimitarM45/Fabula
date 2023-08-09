namespace Fabula.Data;

using Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using System.Reflection;
using System.Reflection.Emit;

public class FabulaDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public FabulaDbContext(DbContextOptions<FabulaDbContext> options)
        : base(options)
    {
    }

    public DbSet<Composition> Compositions { get; set; } = null!;

    public DbSet<Rating> Ratings { get; set; } = null!;

    public DbSet<Genre> Genres { get; set; } = null!;

    public DbSet<Comment> Comments { get; set; } = null!;

    public DbSet<Tag> Tags { get; set; } = null!;

    public DbSet<List> Lists { get; set; } = null!;

    public DbSet<UserLikedComment> UsersLikedComments { get; set; } = null!;

    public DbSet<UserFavoriteComposition> UsersFavoriteCompositions { get; set; } = null!;

    public DbSet<UserFollowedList> UsersFollowedLists { get; set; } = null!;

    public DbSet<UserLikedList> UsersLikedLists { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}