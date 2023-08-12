namespace Fabula.Web.ViewComponents;

using Core.Contracts;
using ViewModels.Comment;

using Microsoft.AspNetCore.Mvc;

public class CommentsPreviewViewComponent : ViewComponent
{
    private readonly ICommentService commentService;

    public CommentsPreviewViewComponent(ICommentService commentService)
    {
        this.commentService = commentService;
    }

    public async Task<IViewComponentResult> InvokeAsync(int numberOfComments, string compositionId)
    {
        IEnumerable<CommentCompositionViewModel> commentViewModels = await commentService.GetForPreviewByIdAsync(compositionId);

        return View("_CommentsPreviewPartial", commentViewModels.Take(numberOfComments));
    }
}
