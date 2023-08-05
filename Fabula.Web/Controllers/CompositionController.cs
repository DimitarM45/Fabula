namespace Fabula.Web.Controllers;

using Core.Contracts;
using Core.ServiceModels;
using ViewModels.Composition;

using static Common.Messages.ErrorMessages.Composition;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

public class CompositionController : BaseController
{
    private readonly IGenreService genreService;

    private readonly ICompositionService compositionService;

    private readonly ICommentService commentService;

    private readonly IRatingService ratingService;

    private readonly IUserService userService;

    private readonly IAccountService accountService;

    public CompositionController(
        IGenreService genreService,
        ICompositionService compositionService,
        ICommentService commentService,
        IRatingService ratingService,
        IUserService userService,
        IAccountService accountService)
    {
        this.genreService = genreService;
        this.compositionService = compositionService;
        this.commentService = commentService;
        this.ratingService = ratingService;
        this.userService = userService;
        this.accountService = accountService;
    }

    [HttpGet]
    [AllowAnonymous]

    public async Task<IActionResult> All([FromQuery] CompositionQueryModel query)
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

            if (compositionReadViewModel == null)
            {
                // TODO: wrap all db requests in a try catch

                return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
            }

            compositionReadViewModel.Genres = await genreService.GetByIdAsync(compositionId);
            compositionReadViewModel.Comments = await commentService.GetForPreviewByIdAsync(compositionId);
            compositionReadViewModel.Ratings = await ratingService.GetRatingsByIdAsync(compositionId);

            //TODO: Add tags if necessary


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

            return RedirectToAction("MyWorks", "User");
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

        if (userId != compositionFormModel?.AuthorId && !await accountService.IsInRoleAsync(userId, "Admin"))
            return RedirectToAction("HandleErrors", "Error", new { statusCode = 401 });

        if (compositionFormModel == null)
            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });

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

    [HttpGet]
    [AllowAnonymous]

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
