namespace Fabula.Web.ViewModels.Rating;

public class RatingViewModel
{
    public string? Id { get; set; }

    public byte Value { get; set; }

    public DateTime PublishedOn { get; set; }

    public string? UserId { get; set; }

    public string? User { get; set; } 
}
