namespace Fabula.Core.Tests.UnitTests;

using Fabula.Web.ViewModels.Genre;

[TestFixture]

public class GenreServiceTests : UnitTestsBase
{
    private IGenreService genreService;

    [SetUp]

    public void SetUp()
    {
        genreService = new GenreService(dbContext);
    }

    [Test]

    public async Task Test_GetAllAsyncShouldReturnAllGenres()
    {
        ICollection<AllGenreViewModel> expectedGenres = dbContext.Genres
            .AsNoTracking()
            .Select(g => new AllGenreViewModel()
            {
                Id = g.Id,
                Name = g.Name,
                CompositionCount = g.Compositions.Count()
            })
            .Take(10)
            .ToList();

        IEnumerable<AllGenreViewModel> actualGenres = await genreService.GetAllAsync();

        Assert.That(expectedGenres.Count == actualGenres.Count());
    }

    [Test]

    public async Task Test_GetAllForSelectAsyncShouldReturnAllGenres()
    {
        ICollection<GenreViewModel> expectedGenres = dbContext.Genres
            .AsNoTracking()
            .Select(g => new GenreViewModel()
            {
                Id = g.Id,
                Name = g.Name
            })
            .Take(10)
            .ToList();

        IEnumerable<GenreViewModel> actualGenres = await genreService.GetAllForSelectAsync();

        Assert.That(actualGenres.Count() == expectedGenres.Count);
    }

    [Test]

    public async Task Test_GetByIdAsyncShouldReturnCorrectGenres()
    {
        ICollection<GenreViewModel> expectedGenres = dbContext.CompositionsGenres
            .AsNoTracking()
            .Where(cg => cg.CompositionId.ToString() == "5d21d1de-c72e-4395-82d7-f35077cf11b7")
            .Select(cg => new GenreViewModel()
            {
                Id = cg.GenreId
            })
            .ToList();

        IEnumerable<GenreViewModel> actualGenres = await genreService.GetByIdAsync("5d21d1de-c72e-4395-82d7-f35077cf11b7");

        Assert.That(expectedGenres.Count == actualGenres.Count());
    }
}
