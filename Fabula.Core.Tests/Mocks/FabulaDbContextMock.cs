namespace Fabula.Core.Tests.Mocks;


public static class FabulaDbContextMock
{
    public static FabulaDbContext Instance()
    {
        DbContextOptions<FabulaDbContext> dbContextOptions = new DbContextOptionsBuilder<FabulaDbContext>()
            .UseInMemoryDatabase("FabulaInMemoryDb"
                + DateTime.Now.Ticks.ToString())
            .Options;

        return new FabulaDbContext(dbContextOptions);
    }
}
