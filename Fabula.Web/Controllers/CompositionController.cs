namespace Fabula.Web.Controllers;

using Core.Contracts;
using ViewModels.Composition;
using Infrastructure.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
        CompositionFormModel compositionFormModel = new CompositionFormModel();

        compositionFormModel.GenresToSelect = await genreService.GetAllForSelectAsync();

        return View(compositionFormModel);
    }

    [HttpPost]
    [ServiceFilter(typeof(HtmlSanitizerFilter))]

    public async Task<IActionResult> Create(CompositionFormModel formModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Invalid input when creating composition!");

            formModel.GenresToSelect = await genreService.GetAllForSelectAsync();

            return View(formModel);
        }

        string userId = GetUserId();

        string compositionId = await compositionService.AddAsync(formModel, userId);

        return RedirectToAction("Read", new { CompositionId = compositionId });
    }

    [HttpGet]
    [AllowAnonymous]

    public async Task<IActionResult> Read(string compositionId)
    {
        CompositionReadViewModel? compositionReadViewModel = await compositionService.GetByIdAsync(compositionId);

        if (compositionReadViewModel == null)
        {
            // TODO: wrap all db requests in a try catch

            return RedirectToAction("HandleErrors", "Error");
        }

        return View(compositionReadViewModel);
    }

    [HttpPost]

    public async Task<IActionResult> Delete(string compositionId)
    {
        try
        {
            await compositionService.DeleteById(compositionId);

            return RedirectToAction("MyCompositions", "User");
        }
        catch (Exception)
        {
            return RedirectToAction("HandleErrors", "Error");
        }
    }

    [HttpGet]

    public async Task<IActionResult> Edit(string compositionId)
    {
        string userId = GetUserId();

        CompositionFormModel? compositionFormModel = await compositionService.GetForEditAsync(compositionId);

        if (userId != compositionFormModel?.AuthorId)
            return RedirectToAction("HandleErrors", "Error", new { statusCode = 401 });

        if (compositionFormModel == null)
            return RedirectToAction("HandleErrors", "Error");

        compositionFormModel.GenresToSelect = await genreService.GetAllForSelectAsync();

        return View(compositionFormModel);
    }

    [HttpPost]
    [ServiceFilter(typeof(HtmlSanitizerFilter))]

    public async Task<IActionResult> Edit(CompositionFormModel formModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Invalid input when creating composition!");

            formModel.GenresToSelect = await genreService.GetAllForSelectAsync();

            return View(formModel);
        }

        await compositionService.UpdateAsync(formModel);

        return RedirectToAction("Read", new { CompositionId = formModel.Id });
    }

    [HttpGet]

    public async Task<IActionResult> Random()
    {
        try
        {
            string randomId = await compositionService.GetRandomIdAsync();

            return RedirectToAction("Read", new { CompositionId = randomId });
        }
        catch (Exception)
        {
            return RedirectToAction("HandleErrors");
        }
    }
}
