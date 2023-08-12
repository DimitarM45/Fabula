namespace Fabula.Core.Tests;

public class UnitTestsBase
{
    protected FabulaDbContext dbContext;

    [OneTimeSetUp]

    public void SetUpBase()
    {
        dbContext = FabulaDbContextMock.Instance();

        SeedDatabase();
    }

    private void SeedDatabase()
    {
        ICollection<Genre> genres = new List<Genre>();

        for (int i = 1; i <= 10; i++)
        {
            Genre genre = new Genre()
            {
                Id = i,
                Name = "Genre" + i
            };

            genres.Add(genre);
        }

        dbContext.AddRange(genres);

        ICollection<Composition> compositions = new List<Composition>();

        for (int i = 1; i <= 10; i++)
        {
            Composition composition = new Composition()
            {
                Id = Guid.Parse("5d21d1de-c72e-4395-82d7-f35077cf11b7"),
                Title = "composition" + i,
                DeletedOn = null,
                CoverUrl = "",
                Content = "",
                hasAdultContent = false,
                Synopsis = "",
                PublishedOn = DateTime.Now,
                Author = new ApplicationUser()
                {
                    Id = Guid.Parse("5d21d1de-c72e-4395-82d7-f35077cf11b7"),
                    UserName = "Username"
                },
                AuthorId = Guid.Parse("5d21d1de-c72e-4395-82d7-f35077cf11b7")
            };
        }

        dbContext.CompositionsGenres.AddRange(genres.Select(g => new CompositionGenre()
        {
            GenreId = g.Id,
            CompositionId = Guid.Parse("5d21d1de-c72e-4395-82d7-f35077cf11b7")
        }).ToList());

        dbContext.SaveChanges();
    }
}
