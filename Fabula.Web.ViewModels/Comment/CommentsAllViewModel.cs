namespace Fabula.Web.ViewModels.Comment;

public class CommentsAllViewModel
{
    public CommentsAllViewModel()
    {
        Comments = new HashSet<CommentViewModel>();    
    }

    public string? CompositionId { get; set; }

    public string? CompositionTitle { get; set; }

    public IEnumerable<CommentViewModel> Comments { get; set; }
}
