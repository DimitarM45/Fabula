namespace Fabula.Web.ViewModels.User;

using List;
using Genre;
using Rating;
using Composition;

public class UserProfileViewModel
{
    public UserProfileViewModel()
    {
        Followers = new HashSet<UserViewModel>();
        WrittenCompositions = new HashSet<CompositionViewModel>();
        FavoriteCompositions = new HashSet<CompositionViewModel>();
        FavoriteGenres = new HashSet<GenreViewModel>();
        Ratings = new HashSet<RatingProfileViewModel>();
        CreatedLists = new HashSet<ListProfileViewModel>();
        FollowedLists = new HashSet<ListProfileViewModel>();
    }

    public string? Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Username { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public string? Bio { get; set; }

    public string? WebsiteUrl { get; set; }

    public IEnumerable<UserViewModel> Followers { get; set; }

    public IEnumerable<CompositionViewModel> WrittenCompositions { get; set; }

    public IEnumerable<CompositionViewModel> FavoriteCompositions { get; set; }

    public IEnumerable<GenreViewModel> FavoriteGenres { get; set; }

    public IEnumerable<RatingProfileViewModel> Ratings { get; set; }

    public IEnumerable<ListProfileViewModel> CreatedLists { get; set; }

    public IEnumerable<ListProfileViewModel> FollowedLists { get; set; }
}
