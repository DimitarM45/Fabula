namespace Fabula.Web.ViewModels.Admin.Composition;

using ViewModels.User;

public class CompositionDashboardViewModel
{
    public string? Id { get; set; }

    public string? Title { get; set; }

    public UserViewModel? Author { get; set; }

    public DateTime PublishedOn { get; set; }

    public DateTime? DeletedOn { get; set; }

    public int Comments { get; set; }

    public int Ratings { get; set; }

    public double Rating { get; set; }
}
