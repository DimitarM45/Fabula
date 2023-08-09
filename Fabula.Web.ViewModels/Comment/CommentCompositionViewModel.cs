namespace Fabula.Web.ViewModels.Comment;

using User;

public class CommentCompositionViewModel
{
    public string? Id { get; set; }

    public string? Content { get; set; }

    public UserViewModel? Author { get; set; }

    public int Likes { get; set; }

    public DateTime PublishedOn { get; set; }

    public int Replies { get; set; }
}
