namespace Fabula.Web.Controllers;

using Core.Contracts;
using ViewModels.Composition;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using System.Security.Claims;

public class CompositionController : BaseController
{
    private readonly IGenreService genreService;

    private readonly ICompositionService compositionService;

    public CompositionController(IGenreService genreService,
        ICompositionService compositionService)
    {
        this.genreService = genreService;
        this.compositionService = compositionService;
    }

    [AllowAnonymous]

    public async Task<IActionResult> All()
    {
        IEnumerable<CompositionAllViewModel> compositionViewModels = await compositionService.GetAllAsync();

        return View(compositionViewModels);
    }

    [HttpGet]

    public async Task<IActionResult> Create()
    {
        CompositionCreateFormModel compositionCreateFormModel = new CompositionCreateFormModel();

        compositionCreateFormModel.GenresToSelect = await genreService.GetAllForSelectAsync();

        return View(compositionCreateFormModel);
    }

    [HttpPost]

    public async Task<IActionResult> Create(CompositionCreateFormModel formModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Invalid input when creating composition!");

            formModel.GenresToSelect = await genreService.GetAllForSelectAsync();

            return View(formModel);
        }

        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        string compositionId = await compositionService.AddAsync(formModel, userId);

        CompositionReadViewModel compositionReadViewModel = await compositionService.GetByIdAsync(compositionId);

        return RedirectToAction("Read", "Composition", compositionReadViewModel);
    }

    [HttpGet]

    public async Task<IActionResult> Read(string compositionId)
    {
        CompositionReadViewModel? compositionReadViewModel = await compositionService.GetByIdAsync(compositionId);

        if (compositionReadViewModel == null)
        {
            // TODO: implement proper error pages
            // TODO: wrap all db requests in a try catch

            return BadRequest();
        }

        return View(compositionReadViewModel);
    }

    public async Task<IActionResult> Delete(string compositionId)
    {
        try
        {
            await compositionService.DeleteById(compositionId);

            return RedirectToAction("MyCompositions", "User");
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}
