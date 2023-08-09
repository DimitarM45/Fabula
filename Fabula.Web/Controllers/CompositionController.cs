namespace Fabula.Web.Controllers;

using Core.Contracts;
using Core.ServiceModels;
using Common.Messages.Enums;
using ViewModels.Composition;

using static Common.Messages.LoggerMessages;
using static Common.Messages.ErrorMessages.Shared;
using static Common.Messages.ErrorMessages.Composition;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using System.Security.Claims;

public class CompositionController : BaseController
{
    private readonly IGenreService genreService;

    private readonly ICompositionService compositionService;

    private readonly IUserService userService;

    private readonly IAccountService accountService;

    private readonly ILogger<CompositionController> logger;

    public CompositionController(
        IGenreService genreService,
        ICompositionService compositionService,
        IUserService userService,
        IAccountService accountService,
        ILogger<CompositionController> logger)
    {
        this.genreService = genreService;
        this.compositionService = compositionService;
        this.userService = userService;
        this.accountService = accountService;
        this.logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]

    public async Task<IActionResult> All([FromQuery] CompositionQueryModel query)
    {
        CompositionQueryModel? compositionQueryModel = null;

        string? userId = userService.GetUserId();

        try
        {
            compositionQueryModel = await compositionService.GetAllAsync(
                    query.SelectedGenres,
                    query.SearchTerm,
                    query.CurrentPage,
                    query.CompositionsPerPage,
                    query.DateSorting,
                    query.RatingSorting);

            compositionQueryModel.Genres = await genreService.GetAllForSelectAsync();
        }
        catch (Exception e)
        {
            logger.LogWarning(string.Format(Warning,
                e.Message, 
                e.StackTrace, 
                userId == null ? NonExistentUser : userId,
                "/" + ControllerContext.ActionDescriptor.ControllerName + 
                "/" + ControllerContext.ActionDescriptor.ActionName,
                DateTime.Now));

            TempData[NotificationType.ErrorMessage.ToString()] = FailedResourceRetrieval;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }

        compositionQueryModel.CurrentPage = query.CurrentPage;

        return View(compositionQueryModel);
    }

    [HttpGet]

    public async Task<IActionResult> Create()
    {
        CompositionFormModel compositionFormModel = new CompositionFormModel();

        string? userId = userService.GetUserId();

        try
        {
            compositionFormModel.GenresToSelect = await genreService.GetAllForSelectAsync();
        }
        catch (Exception e)
        {
            logger.LogWarning(string.Format(Warning,
                e.Message,
                e.StackTrace,
                userId == null ? NonExistentUser : userId,
                "/" + ControllerContext.ActionDescriptor.ControllerName +
                "/" + ControllerContext.ActionDescriptor.ActionName,
                DateTime.Now));

            TempData[NotificationType.ErrorMessage.ToString()] = FailedResourceRetrieval;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }

        return View(compositionFormModel);
    }

    [HttpPost]

    public async Task<IActionResult> Create(CompositionFormModel formModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                formModel.GenresToSelect = await genreService.GetAllForSelectAsync();

                ModelState.AddModelError("", "Invalid input when creating composition!");

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
        catch (Exception)
        {
            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }

    [HttpGet]
    [AllowAnonymous]

    public async Task<IActionResult> Read(string compositionId)
    {
        string? userId = userService.GetUserId();

        try
        {
            CompositionReadViewModel? compositionReadViewModel = await compositionService.GetByIdAsync(compositionId);

            if (compositionReadViewModel == null)
                return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });

            compositionReadViewModel.Genres = await genreService.GetByIdAsync(compositionId);

            //TODO: Add comments, ratings, comments, tags if necessary

            return View(compositionReadViewModel);
        }
        catch (Exception e)
        {
            logger.LogWarning(string.Format(Warning,
                e.Message,
                e.StackTrace,
                userId == null ? NonExistentUser : userId,
                "/" + ControllerContext.ActionDescriptor.ControllerName +
                "/" + ControllerContext.ActionDescriptor.ActionName,
                DateTime.Now));

            TempData[NotificationType.ErrorMessage.ToString()] = FailedResourceRetrieval;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }

    [HttpPost]

    public async Task<IActionResult> Delete(string compositionId)
    {
        try
        {
            await compositionService.DeleteByIdAsync(compositionId);

            return RedirectToAction("Works", "User", new { UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) });
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

        try
        {
            CompositionFormModel? compositionFormModel = await compositionService.GetForEditAsync(compositionId);

            if (compositionFormModel == null)
                return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });

            if (userId != compositionFormModel.AuthorId && !await accountService.IsInRoleAsync(userId, "Admin"))
                return RedirectToAction("HandleErrors", "Error", new { statusCode = 401 });

            compositionFormModel.GenresToSelect = await genreService.GetAllForSelectAsync();

            return View(compositionFormModel);
        }
        catch (Exception)
        {
            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }

    [HttpPost]

    public async Task<IActionResult> Edit(CompositionFormModel formModel)
    {
        try
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
        catch (Exception)
        {
            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
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
            return RedirectToAction("Read", new { CompositionId = compositionId });

        TempData["FailedRestore"] = FailedRestoreErrorMessage;

        return View("MyWorks", "User");
    }
}
