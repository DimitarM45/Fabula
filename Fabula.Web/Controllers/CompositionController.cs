namespace Fabula.Web.Controllers;

using Core.Contracts;
using Core.ServiceModels;
using ViewModels.Composition;
using Infrastructure.Utilities;

using static Common.GlobalConstants;
using static Common.Messages.LoggerMessages;
using static Common.Messages.NotificationTypes;
using static Common.Messages.ErrorMessages.Shared;
using static Common.Messages.SuccessMessages.Shared;
using static Common.Messages.ErrorMessages.Composition;
using static Common.Messages.ErrorMessages.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

public class CompositionController : BaseController
{
    private readonly IGenreService genreService;

    private readonly ICompositionService compositionService;

    private readonly IUserService userService;

    private readonly IAccountService accountService;

    private readonly ILogger logger;

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

        try
        {
            compositionQueryModel = await compositionService.GetAllAsync(
                    query.SelectedGenres,
                    query.UserId,
                    query.SearchTerm,
                    query.CurrentPage,
                    query.CompositionsPerPage,
                    query.DateSorting,
                    query.RatingSorting);

            if (query.UserId != null)
            {
                compositionQueryModel.UserId = query.UserId;
                compositionQueryModel.Username = await userService.GetUsernameAsync(query.UserId);
            }

            compositionQueryModel.Genres = await genreService.GetAllForSelectAsync();
        }
        catch (Exception e)
        {
            string? userId = userService.GetUserId();

            logger.LogWarning(
                LogMessageFormatter.FormatWarningLogMessage(Warning, e, userId, GetControllerName(), GetActionName()));

            TempData[ErrorNotification] = GeneralErrorMessage;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }

        compositionQueryModel.CurrentPage = query.CurrentPage;

        return View(compositionQueryModel);
    }

    [HttpGet]

    public async Task<IActionResult> Create()
    {
        CompositionFormModel compositionFormModel = new CompositionFormModel();

        try
        {
            compositionFormModel.GenresToSelect = await genreService.GetAllForSelectAsync();
        }
        catch (Exception e)
        {
            string? userId = userService.GetUserId();

            logger.LogWarning(
                LogMessageFormatter.FormatWarningLogMessage(Warning, e, userId, GetControllerName(), GetActionName()));

            TempData[ErrorNotification] = GeneralErrorMessage;

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

                TempData[ErrorNotification] = InvalidInputDataErrorMessage;

                return View(formModel);
            }

            string? userId = userService.GetUserId();

            if (userId == null)
            {
                formModel.GenresToSelect = await genreService.GetAllForSelectAsync();

                TempData[ErrorNotification] = FailedCreatingCompositionErrorMessage;

                return View(formModel);
            }

            string compositionId = await compositionService.AddAsync(formModel, userId);

            TempData[SuccessNotification] =
                string.Format(SuccessfulResourceCreationMessage,
                GetControllerName().ToLower(),
                formModel.Title);

            return RedirectToAction("Read", new { CompositionId = compositionId });
        }
        catch (Exception e)
        {
            string? userId = userService.GetUserId();

            logger.LogWarning(
                LogMessageFormatter.FormatWarningLogMessage(Warning, e, userId, GetControllerName(), GetActionName()));

            TempData[ErrorNotification] = GeneralErrorMessage;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
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
                TempData[ErrorNotification] =
                    string.Format(ResourceNotFoundErrorMessage, GetControllerName().ToLower());

                return RedirectToAction("HandleErrors", "Error", new { statusCode = 404 });
            }

            compositionReadViewModel.Genres = await genreService.GetByIdAsync(compositionId);

            //TODO: Add comments, ratings, comments, tags if necessary

            return View(compositionReadViewModel);
        }
        catch (Exception e)
        {
            string? userId = userService.GetUserId();

            logger.LogWarning(
                LogMessageFormatter.FormatWarningLogMessage(Warning, e, userId, GetControllerName(), GetActionName()));

            TempData[ErrorNotification] = GeneralErrorMessage;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }

    [HttpPost]

    public async Task<IActionResult> Delete(string compositionId)
    {
        try
        {
            await compositionService.DeleteByIdAsync(compositionId);

            TempData[SuccessNotification] =
                string.Format(SuccessfulResourceDeletionMessage, GetControllerName().ToLower());

            return RedirectToAction("User", "Deleted");
        }
        catch (Exception e)
        {
            string? userId = userService.GetUserId();

            logger.LogWarning(
                LogMessageFormatter.FormatWarningLogMessage(Warning, e, userId, GetControllerName(), GetActionName()));

            TempData[ErrorNotification] = GeneralErrorMessage;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }

    [HttpGet]

    public async Task<IActionResult> Edit(string compositionId)
    {
        string? userId = userService.GetUserId();

        if (userId == null)
        {
            TempData[ErrorNotification] = FailedEditingCompositionErrorMessage;

            return View();
        }

        try
        {
            CompositionFormModel? compositionFormModel = await compositionService.GetForEditAsync(compositionId);

            if (compositionFormModel == null)
            {
                TempData[ErrorNotification] = 
                    string.Format(ResourceNotFoundErrorMessage, GetControllerName().ToLower());

                return RedirectToAction("HandleErrors", "Error", new { statusCode = 404 });
            }

            if (userId != compositionFormModel.AuthorId && !await accountService.IsInRoleAsync(userId, AdminRoleName))
            {
                TempData[WarningNotification] = UnauthorizedErrorMessage;

                return RedirectToAction("HandleErrors", "Error", new { statusCode = 401 });
            }

            compositionFormModel.GenresToSelect = await genreService.GetAllForSelectAsync();

            return View(compositionFormModel);
        }
        catch (Exception e)
        {
            logger.LogWarning(
                LogMessageFormatter.FormatWarningLogMessage(Warning, e, userId, GetControllerName(), GetActionName()));

            TempData[ErrorNotification] = GeneralErrorMessage;

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
                ModelState.AddModelError(string.Empty, InvalidInputDataErrorMessage);

                formModel.GenresToSelect = await genreService.GetAllForSelectAsync();

                TempData[ErrorNotification] = InvalidInputDataErrorMessage;

                return View(formModel);
            }

            await compositionService.UpdateAsync(formModel);

            TempData[SuccessNotification] =
                string.Format(SuccessfulResourceUpdateMessage, 
                    GetControllerName().ToLower(), 
                    formModel.Title);

            return RedirectToAction("Read", new { CompositionId = formModel.Id });
        }
        catch (Exception e)
        {
            string? userId = userService.GetUserId();

            logger.LogWarning(
                LogMessageFormatter.FormatWarningLogMessage(Warning, e, userId, GetControllerName(), GetActionName()));

            TempData[ErrorNotification] = GeneralErrorMessage;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }

    [HttpGet]
    [AllowAnonymous]

    public async Task<IActionResult> Random()
    {
        try
        {
            string? randomId = await compositionService.GetRandomIdAsync();

            if (randomId == null)
            {
                TempData[ErrorNotification] =
                    string.Format(ResourceNotFoundErrorMessage, GetControllerName().ToLower());

                return RedirectToAction("HandleErrors", "Error", new { statusCode = 404 });
            }

            return RedirectToAction("Read", new { CompositionId = randomId });
        }
        catch (Exception e)
        {
            string? userId = userService.GetUserId();

            logger.LogWarning(
                LogMessageFormatter.FormatWarningLogMessage(Warning, e, userId, GetControllerName(), GetActionName()));

            TempData[ErrorNotification] = GeneralErrorMessage;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }

    [HttpGet]

    public async Task<IActionResult> Restore(string compositionId)
    {
        bool isRestored = false;

        string? userId = userService.GetUserId();

        try
        {
            isRestored = await compositionService.RestoreByIdAsync(compositionId);
        }
        catch (Exception e)
        {
            logger.LogWarning(
                LogMessageFormatter.FormatWarningLogMessage(Warning, e, userId, GetControllerName(), GetActionName()));

            TempData[ErrorNotification] = GeneralErrorMessage;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }

        if (isRestored)
        {
            TempData[SuccessNotification] = SuccessfulResourceRestorationMessage;

            return RedirectToAction("Read", new { CompositionId = compositionId });
        }

        TempData[ErrorNotification] = FailedRestoreErrorMessage;

        return RedirectToAction("Deleted");
    }
}
