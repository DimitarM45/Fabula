namespace Fabula.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class FabulaDbContext : IdentityDbContext
{
    public FabulaDbContext(DbContextOptions<FabulaDbContext> options)
        : base(options)
    {
    }
}