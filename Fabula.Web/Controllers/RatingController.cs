namespace Fabula.Web.Controllers;

using Microsoft.AspNetCore.Mvc;

using System.Web.Mvc;

public class RatingController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }

    [ChildActionOnly]

    public async Task<IActionResult> CompositionPreview()
    {
        return View("_RatingPreviewPartial");
    }
}
