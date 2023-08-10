namespace Fabula.Web.ViewModels.User;

using Genre;

public class UserProfileViewModel
{
    public UserProfileViewModel()
    {
        FavoriteGenres = new HashSet<GenreViewModel>();
    }

    public string? Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Username { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public string? Bio { get; set; }

    public string? WebsiteUrl { get; set; }

    public int Followers { get; set; }

    public int WrittenCompositions { get; set; }

    public int FavoriteCompositions { get; set; }

    public int Ratings { get; set; }

    public int CreatedLists { get; set; }

    public int FollowedLists { get; set; }

    public IEnumerable<GenreViewModel> FavoriteGenres { get; set; }
}
