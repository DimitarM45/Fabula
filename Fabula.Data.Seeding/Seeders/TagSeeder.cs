namespace Fabula.Data.Seeding.Seeders;

using Models;

public static class TagSeeder
{
    public static IEnumerable<Tag> SeedTags()
    {
        ICollection<Tag> tags = new List<Tag>();

        string[] tagNames =
        {
            "Biography",
            "Memoir",
            "Non-fiction",
            "Self-Help",
            "Travel",
            "Science",
            "Fiction",
            "Satire",
            "LGBTQ+",
            "Graphic Novel",
            "Food",
            "Music",
            "Finance",
            "Technology",
            "Educational",
            "Nature",
            "Religion",
            "Psychology",
            "Anthology",
            "War",
            "Supernatural",
            "Coming of Age",
            "Family",
            "Magic",
            "Inspirational",
            "Suspense",
            "Thrilling",
            "Intrigue",
            "Spiritual",
            "Artistic",
            "Meditative",
            "Environmental",
            "Journal",
            "Guide",
            "Reference",
            "Health",
            "Fitness",
            "Fashion",
            "Cookbook",
            "Crafts",
            "Gardening",
            "Architecture",
            "Design",
            "Parenting",
            "Relationships",
            "Education",
            "Philosophy",
            "Science Fiction",
            "Business",
            "Crime",
            "Historical"
        };

        for (int i = 0; i < tagNames.Length; i++)
            tags.Add(new Tag() { Id = i + 1, Name = tagNames[i] });

        return tags;
    }
}
