namespace Fabula.Web.ViewModels.Comment;

public class CommentViewModel
{
    public string? Id { get; set; }

    public string? Content { get; set; }

    public string? AuthorId { get; set; }

    public string? Author { get; set; }

    public int Likes { get; set; }

    public Guid CompositionId { get; set; }

    public DateTime PublishedOn { get; set; }
}
