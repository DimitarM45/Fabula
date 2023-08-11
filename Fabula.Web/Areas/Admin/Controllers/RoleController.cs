namespace Fabula.Web.Areas.Admin.Controllers;

using Core.Contracts;
using ViewModels.Admin.Role;
using Common.Messages.Enums;

using static Common.Messages.LoggerMessages;
using static Common.Messages.ErrorMessages.Shared;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Security.Claims;

public class RoleController : BaseController
{
    private readonly IRoleService roleService;

    private readonly ILogger logger;

    public RoleController(IRoleService roleService, ILogger logger)
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

            logger.LogWarning(string.Format(Warning,
                e.Message,
                e.StackTrace,
                userId == null ? NonExistentUser : userId,
                "/" + ControllerContext.ActionDescriptor.ControllerName +
                "/" + ControllerContext.ActionDescriptor.ActionName,
                DateTime.Now));

            TempData[NotificationType.ErrorMessage.ToString()] = GeneralErrorMessage;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }

    [HttpPost]

    public async Task<IActionResult> Create(RoleFormModel roleFormModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, InvalidInputDataErrorMessage);

            TempData[NotificationType.ErrorMessage.ToString()] = InvalidInputDataErrorMessage;

            return RedirectToAction("All");
        }

        try
        {
            await roleService.AddAsync(roleFormModel);

            return RedirectToAction("All");
        }
        catch (Exception e)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            logger.LogWarning(string.Format(Warning,
                e.Message,
                e.StackTrace,
                userId == null ? NonExistentUser : userId,
                "/" + ControllerContext.ActionDescriptor.ControllerName +
                "/" + ControllerContext.ActionDescriptor.ActionName,
                DateTime.Now));

            TempData[NotificationType.ErrorMessage.ToString()] = GeneralErrorMessage;

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
                return RedirectToAction();
            }

            return View(roleFormModel);
        }
        catch (Exception e)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            logger.LogWarning(string.Format(Warning,
                e.Message,
                e.StackTrace,
                userId == null ? NonExistentUser : userId,
                "/" + ControllerContext.ActionDescriptor.ControllerName +
                "/" + ControllerContext.ActionDescriptor.ActionName,
                DateTime.Now));

            TempData[NotificationType.ErrorMessage.ToString()] = GeneralErrorMessage;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }

    [HttpPost]

    public async Task<IActionResult> Edit(RoleFormModel roleFormModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, InvalidInputDataErrorMessage);

            TempData[NotificationType.ErrorMessage.ToString()] = InvalidInputDataErrorMessage;

            return View(roleFormModel);
        }

        try
        {
            await roleService.UpdateAsync(roleFormModel);

            return RedirectToAction("All");
        }
        catch (Exception e)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            logger.LogWarning(string.Format(Warning,
                e.Message,
                e.StackTrace,
                userId == null ? NonExistentUser : userId,
                "/" + ControllerContext.ActionDescriptor.ControllerName +
                "/" + ControllerContext.ActionDescriptor.ActionName,
                DateTime.Now));

            TempData[NotificationType.ErrorMessage.ToString()] = GeneralErrorMessage;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }

    [HttpPost]

    public async Task<IActionResult> Delete(string roleId)
    {
        try
        {
            await roleService.DeleteAsync(roleId);

            return RedirectToAction("All");
        }
        catch (Exception e)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            logger.LogWarning(string.Format(Warning,
                e.Message,
                e.StackTrace,
                userId == null ? NonExistentUser : userId,
                "/" + ControllerContext.ActionDescriptor.ControllerName +
                "/" + ControllerContext.ActionDescriptor.ActionName,
                DateTime.Now));

            TempData[NotificationType.ErrorMessage.ToString()] = GeneralErrorMessage;

            return RedirectToAction("HandleErrors", "Error", new { statusCode = 500 });
        }
    }
}
