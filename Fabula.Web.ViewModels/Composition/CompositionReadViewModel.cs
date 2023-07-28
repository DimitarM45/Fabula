namespace Fabula.Web.ViewModels.Composition;

using Genre;
using Rating;
using Comment;

public class CompositionReadViewModel
{
    public CompositionReadViewModel()
    {
        Tags = new HashSet<string>();
        Genres = new HashSet<GenreViewModel>();
        Ratings = new HashSet<RatingViewModel>();
        Comments = new HashSet<CommentCompositionViewModel>();
    }

    public string? Id { get; set; }

    public string? Title { get; set; }

    public string? CoverUrl { get; set; }

    public string? Content { get; set; } 

    public string? Synopsis { get; set; }

    public string? AuthorId { get; set; }

    public string? Author { get; set; }

    public int Favorites { get; set; }

    public DateTime PublishedOn { get; set; }

    public bool hasAdultContent { get; set; }

    public ICollection<string> Tags { get; set; }

    public IEnumerable<GenreViewModel> Genres { get; set; }

    public IEnumerable<RatingViewModel> Ratings { get; set; }

    public IEnumerable<CommentCompositionViewModel> Comments { get; set; }
}
