namespace Fabula.Web.ViewModels.Composition;

using User;
using Genre;

public class CompositionViewModel
{
    public CompositionViewModel()
    {
        Genres = new HashSet<GenreViewModel>();    
    }

    public string? Id { get; set; }

    public string? Title { get; set; } 

    public string? CoverUrl { get; set; } 

    public string? Synopsis { get; set; }

    public UserViewModel? Author { get; set; }

    public DateTime PublishedOn { get; set; }

    public bool HasAdultContent { get; set; }

    public double? Rating { get; set; }

    public IEnumerable<GenreViewModel> Genres { get; set; }
}
