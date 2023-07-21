namespace Fabula.Web.ViewModels.Rating;

using User;

public class RatingViewModel
{
    public string? Id { get; set; }

    public byte Value { get; set; }

    public UserViewModel? Author { get; set; }

    public DateTime PublishedOn { get; set; }
}
