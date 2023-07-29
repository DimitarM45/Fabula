namespace Fabula.Data.Seeding.Seeders;

using Microsoft.AspNetCore.Identity;

public static class RoleSeeder
{
    public static IEnumerable<IdentityRole<Guid>> SeedRoles()
    {
        IEnumerable<IdentityRole<Guid>> roles = new IdentityRole<Guid>[]
        {
            new IdentityRole<Guid>()
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },

            new IdentityRole<Guid>()
            {
                Id = Guid.NewGuid(),
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            }
        };

        return roles;
    }
}
