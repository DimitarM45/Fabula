namespace Fabula.Web.Controllers;

using Core.Contracts;
using ViewModels.Composition;

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
            // TODO: implement proper error pages
            // TODO: wrap all db requests in a try catch

            return BadRequest();
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
            // CUSTOM ERROR PAGE REDIRECTION

            return BadRequest();
        }
    }

    [HttpGet]

    public async Task<IActionResult> Edit(string compositionId)
    {
        string userId = GetUserId();

        CompositionFormModel? compositionFormModel = await compositionService.GetForEditAsync(compositionId);

        // TODO: CUSTOM ERROR PAGE

        if (userId != compositionFormModel?.AuthorId)
            return BadRequest();

        if (compositionFormModel == null)
        {
            // TODO: CUSTOM ERROR PAGE REDIRECTION

            return BadRequest();
        }

        compositionFormModel.GenresToSelect = await genreService.GetAllForSelectAsync();

        return View(compositionFormModel);
    }

    [HttpPost]

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
}
