namespace Fabula.Web.Controllers;

using Microsoft.AspNetCore.Mvc;

public class RatingController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }
}
