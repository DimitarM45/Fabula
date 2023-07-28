namespace Fabula.Web.ViewModels.List;

using Fabula.Web.ViewModels.Composition;

public class ListProfileViewModel
{
    public ListProfileViewModel()
    {
        Compositions = new HashSet<CompositionProfileViewModel>();
    }

    public string? Id { get; set; }

    public string? Title { get; set; } 

    public string? Description { get; set; } 

    public bool hasAdultContent { get; set; }

    public IEnumerable<CompositionProfileViewModel> Compositions { get; set; }

    public int Likes { get; set; }

    public int Followers { get; set; }
}
