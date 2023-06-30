namespace Fabula.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]

public class CompositionController : Controller
{
    public async Task<IActionResult> All()
    {

    }
}
