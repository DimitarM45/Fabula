namespace Fabula.Data.Seeding.Seeders;

using Models;

public static class GenreSeeder
{
    public static IEnumerable<Genre> SeedGenres()
    {
        string[] genreNames = {
            "Mystery",
            "Romance",
            "Science Fiction",
            "Fantasy",
            "Thriller",
            "Horror",
            "Historical Fiction",
            "Poetry",
            "Drama",
            "Comedy",
            "Action",
            "Adventure",
            "Crime",
            "Satire",
            "Young Adult",
            "Fairy Tale",
            "Western",
            "Paranormal",
            "Urban Fantasy",
            "Steamy Romance",
            "Time Travel",
            "Political Thriller",
            "Noir",
            "Detective",
            "Humor",
            "Children's",
            "Historical Romance",
            "Psychological",
            "Epic",
            "Classic",
            "Short Story",
            "Mythological",
            "Space Opera",
            "Military Fiction",
            "Cyberpunk",
            "Post-Apocalyptic",
            "Chick Lit",
            "Sports",
            "Gothic",
            "Art",
            "Business",
            "Economics",
            "Politics"
        };

        ICollection<Genre> genres = new List<Genre>();

        for (int i = 0; i < genreNames.Length; i++)
            genres.Add(new Genre() { Id = i + 1, Name = genreNames[i] });

        return genres;
    }
}
