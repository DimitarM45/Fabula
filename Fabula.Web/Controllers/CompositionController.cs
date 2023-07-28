namespace Fabula.Web.Controllers;

using Core.Contracts;
using ViewModels.Composition;
using Infrastructure.Filters;
using static Fabula.Common.Messages.ErrorMessages.Composition;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

public class CompositionController : BaseController
{
    private readonly IGenreService genreService;

    private readonly ICompositionService compositionService;

    private readonly ICommentService commentService;

    private readonly IRatingService ratingService;

    private readonly IUserService userService;

    public CompositionController(IGenreService genreService,
        ICompositionService compositionService,
        ICommentService commentService,
        IRatingService ratingService,
        IUserService userService)
    {
        this.genreService = genreService;
        this.compositionService = compositionService;
        this.commentService = commentService;
        this.ratingService = ratingService;
        this.userService = userService;
    }

    [HttpGet]
    [AllowAnonymous]

    public async Task<IActionResult> All()
    {
        IEnumerable<CompositionViewModel> compositionViewModels = await compositionService.GetAllAsync();

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

        string? userId = userService.GetUserId();

        if (userId == null)
        {
            formModel.GenresToSelect = await genreService.GetAllForSelectAsync();

            TempData["Error"] = FailedCreatingCompositionErrorMessage;

            return View(formModel);
        }

        string compositionId = await compositionService.AddAsync(formModel, userId);

        return RedirectToAction("Read", new { CompositionId = compositionId });
    }

    [HttpGet]
    [AllowAnonymous]

    public async Task<IActionResult> Read(string compositionId)
    {
        try
        {
            CompositionReadViewModel? compositionReadViewModel = await compositionService.GetByIdAsync(compositionId);

            compositionReadViewModel.Genres = await genreService.GetByIdAsync(compositionId);
            compositionReadViewModel.Comments = await commentService.GetForPreviewByIdAsync(compositionId);
            compositionReadViewModel.Ratings = await ratingService.GetRatingsByIdAsync(compositionId);

            //TODO: Add tags if necessary

            if (compositionReadViewModel == null)
            {
                // TODO: wrap all db requests in a try catch

                return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
            }

            return View(compositionReadViewModel);
        }
        catch
        {
            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }

    [HttpPost]

    public async Task<IActionResult> Delete(string compositionId)
    {
        try
        {
            await compositionService.DeleteByIdAsync(compositionId);

            return RedirectToAction("MyWorks");
        }
        catch (Exception)
        {
            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }

    [HttpGet]

    public async Task<IActionResult> Edit(string compositionId)
    {
        string? userId = userService.GetUserId();

        if (userId == null)
        {
            TempData["NoUser"] = FailedEditingCompositionErrorMessage;

            return View();
        }

        CompositionFormModel? compositionFormModel = await compositionService.GetForEditAsync(compositionId);

        if (userId != compositionFormModel?.AuthorId)
            return RedirectToAction("HandleErrors", "Error", new { statusCode = 401 });

        if (compositionFormModel == null)
            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });

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
        string? randomId = null;

        try
        {
            randomId = await compositionService.GetRandomIdAsync();
        }
        catch (Exception)
        {
            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }

        if (randomId == null)
            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });

        return RedirectToAction("Read", new { CompositionId = randomId });
    }

    [HttpGet]

    public async Task<IActionResult> Restore(string compositionId)
    {
        bool isRestored = false;

        try
        {
            isRestored = await compositionService.RestoreByIdAsync(compositionId);
        }
        catch (Exception)
        {
            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }

        if (isRestored)
            return RedirectToAction("Read");

        TempData["FailedRestore"] = FailedRestoreErrorMessage;

        return View("MyWorks", "User");
    }
}
