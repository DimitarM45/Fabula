namespace Fabula.Web.ViewModels.Composition;

public class CompositionProfileViewModel
{
    public string? Id { get; set; }

    public string? Title { get; set; }

    public string? Synopsis { get; set; }

    public string? CoverUrl { get; set; }

    public bool HasAdultContent { get; set; }

    public double? Rating { get; set; }
}
