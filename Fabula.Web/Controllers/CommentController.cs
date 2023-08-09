namespace Fabula.Web.Controllers;

using Core.Contracts;
using ViewModels.Comment;

using Microsoft.AspNetCore.Mvc;

public class CommentController : BaseController
{
    private readonly ICommentService commentService;

    public CommentController(ICommentService commentService)
    {
        this.commentService = commentService;
    }

    public async Task<IActionResult> All(string compositionId)
    {
        try
        {
            CommentsAllViewModel? commentViewModels = await commentService.GetAllByIdAsync(compositionId);

            if (commentViewModels == null)
                return RedirectToAction("HandleErrors", "Error", new { statusCode = 401 });

            return View(commentViewModels);
        }
        catch (Exception)
        {
            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }
}
