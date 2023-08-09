namespace Fabula.Web.ViewModels.Comment;

using User;

public class CommentViewModel
{
    public CommentViewModel()
    {
        Replies = new HashSet<CommentViewModel>();    
    }

    public string? Id { get; set; }

    public string? Content { get; set; }

    public UserViewModel? Author { get; set; }

    public int Likes { get; set; }

    public DateTime PublishedOn { get; set; }

    public IEnumerable<CommentViewModel> Replies { get; set; }
}
