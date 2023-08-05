namespace Fabula.Web.Controllers;

using Microsoft.AspNetCore.Mvc;

using System.Web.Mvc;

public class CommentController : BaseController
{
    public async Task<IActionResult> All(string compositionId)
    {
        return View();
    }

    [ChildActionOnly]

    public async Task<IActionResult> CompositionPreview(string compositionId)
    {
        return View("_CommentPreviewPartial");
    }
}
