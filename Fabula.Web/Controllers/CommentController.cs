namespace Fabula.Web.Controllers;

using Microsoft.AspNetCore.Mvc;

public class CommentController : BaseController
{
    public async Task<IActionResult> All(string compositionId)
    {
        return View();
    }
}
