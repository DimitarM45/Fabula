namespace Fabula.Web.Controllers;

using Core.Contracts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Fabula.Web.ViewModels.Composition;

public class CompositionController : BaseController
{
    private readonly ICompositionService compositionService;

    public CompositionController(ICompositionService compositionService)
    {
        this.compositionService = compositionService;
    }

    [AllowAnonymous]

    public async Task<IActionResult> All()
    {
        IEnumerable<CompositionAllViewModel> compositionViewModels = await compositionService.GetAllAsync();

        return View(compositionViewModels);
    }

    [AllowAnonymous]

    public async Task<IActionResult> AllByGenre(string genreId)
    {
        return View();
    }
}
