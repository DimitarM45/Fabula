namespace Fabula.Web.Controllers;

using Core.Contracts;
using ViewModels.Comment;
using Infrastructure.Utilities;

using static Common.Messages.LoggerMessages;
using static Common.Messages.NotificationTypes;
using static Common.Messages.ErrorMessages.Shared;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Security.Claims;

public class CommentController : BaseController
{
    private readonly ICommentService commentService;

    private readonly ILogger logger;

    public CommentController(ICommentService commentService, ILogger<CommentController> logger)
    {
        this.commentService = commentService;
        this.logger = logger;
    }

    public async Task<IActionResult> All(string compositionId)
    {
        try
        {
            CommentsAllViewModel? commentViewModels = await commentService.GetAllByIdAsync(compositionId);

            if (commentViewModels == null)
            {
                TempData[WarningNotification] =
                    string.Format(ResourceNotFoundErrorMessage, GetControllerName().ToLower());

                return RedirectToAction("HandleErrors", "Error", new { statusCode = 404 });
            }

            return View(commentViewModels);
        }
        catch (Exception e)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            logger.LogWarning(
                LogMessageFormatter.FormatWarningLogMessage(Warning, e, userId, GetControllerName(), GetActionName()));

            TempData[ErrorNotification] = GeneralErrorMessage;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }
}
