namespace Fabula.Web.ViewModels.Admin.User;

public class UserDashboardViewModel
{
    public string? Id { get; set; }

    public string? Username { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public string? Role { get; set; }

    public int WrittenCompositions { get; set; }

    public int WrittenComments { get; set; }

    public int WrittenReviews { get; set; }
}
