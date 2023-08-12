namespace Fabula.Web.Areas.Admin.Controllers;

using Core.Contracts;
using ViewModels.Admin.Role;
using Infrastructure.Utilities; 

using static Common.Messages.LoggerMessages;
using static Common.Messages.NotificationTypes;
using static Common.Messages.ErrorMessages.Shared;
using static Common.Messages.SuccessMessages.Shared;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Security.Claims;

public class RoleController : BaseController
{
    private readonly IRoleService roleService;

    private readonly ILogger logger;

    public RoleController(IRoleService roleService, ILogger<RoleController> logger)
    {
        this.roleService = roleService;
        this.logger = logger;
    }

    [HttpGet]

    public async Task<IActionResult> All()
    {
        try
        {
            RoleAllViewModel roleAllViewModel = await roleService.GetAllAsync();

            return View(roleAllViewModel);
        }
        catch (Exception e)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            logger.LogWarning(
                LogMessageFormatter.FormatWarningLogMessage(Warning, e, userId, GetControllerName(), GetActionName()));

            TempData[ErrorNotification] = GeneralErrorMessage;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }

    [HttpPost]

    public async Task<IActionResult> Create(RoleFormModel roleFormModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, InvalidInputDataErrorMessage);

            TempData[ErrorNotification] = InvalidInputDataErrorMessage;

            return RedirectToAction("All");
        }

        try
        {
            await roleService.AddAsync(roleFormModel);

            TempData[SuccessNotification] = 
                string.Format(SuccessfulResourceCreationMessage,
                    GetControllerName().ToLower(),
                    roleFormModel.Name);

            return RedirectToAction("All");
        }
        catch (Exception e)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            logger.LogWarning(
                LogMessageFormatter.FormatWarningLogMessage(Warning, e, userId, GetControllerName(), GetActionName()));

            TempData[ErrorNotification] = GeneralErrorMessage;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }

    [HttpGet]

    public async Task<IActionResult> Edit(string roleId)
    {
        try
        {
            RoleFormModel? roleFormModel = await roleService.GetForEditAsync(roleId);

            if (roleFormModel == null)
            {
                TempData[WarningNotification] = 
                    string.Format(ResourceNotFoundErrorMessage, GetControllerName().ToLower());

                return RedirectToAction("HandleErrors", "Error", new { statusCode = 404 });
            }

            return View(roleFormModel);
        }
        catch (Exception e)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            logger.LogWarning(
                LogMessageFormatter.FormatWarningLogMessage(Warning, e, userId, GetControllerName(), GetActionName()));

            TempData[ErrorNotification] = GeneralErrorMessage;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }

    [HttpPost]

    public async Task<IActionResult> Edit(RoleFormModel roleFormModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, InvalidInputDataErrorMessage);

            TempData[ErrorNotification] = InvalidInputDataErrorMessage;

            return View(roleFormModel);
        }

        try
        {
            await roleService.UpdateAsync(roleFormModel);

            TempData[SuccessNotification] =
                string.Format(SuccessfulResourceUpdateMessage, 
                    GetControllerName().ToLower(), 
                    roleFormModel.Name);

            return RedirectToAction("All");
        }
        catch (Exception e)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            logger.LogWarning(
                LogMessageFormatter.FormatWarningLogMessage(Warning, e, userId, GetControllerName(), GetActionName()));

            TempData[ErrorNotification] = GeneralErrorMessage;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }

    [HttpPost]

    public async Task<IActionResult> Delete(string roleId)
    {
        try
        {
            await roleService.DeleteAsync(roleId);

            TempData[SuccessNotification] =
                string.Format(SuccessfulResourceDeletionMessage, GetControllerName().ToLower());

            return RedirectToAction("All");
        }
        catch (Exception e)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            logger.LogWarning(
                LogMessageFormatter.FormatWarningLogMessage(Warning, e, userId, GetControllerName(), GetActionName()));

            TempData[ErrorNotification] = GeneralErrorMessage;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }
}
