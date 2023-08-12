namespace Fabula.Web.Controllers;

using Core.Contracts;
using ViewModels.Account;
using Infrastructure.Utilities;

using static Common.GlobalConstants;
using static Common.Messages.LoggerMessages;
using static Common.Messages.NotificationTypes;
using static Common.Messages.ErrorMessages.Shared;
using static Common.Messages.ErrorMessages.Authentication;
using static Common.Messages.SuccessMessages.Authentication;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

using System.Security.Claims;

public class AccountController : BaseController
{
    private IAccountService accountService;

    private ILogger logger;

    public AccountController(IAccountService accountService, ILogger<AccountController> logger)
    {
        this.accountService = accountService;
        this.logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]

    public async Task<IActionResult> Register(string? returnUrl)
    {
        RegisterFormModel registerFormModel = await accountService.GetRegisterModelAsync(returnUrl);

        return View(registerFormModel);
    }

    [HttpPost]
    [AllowAnonymous]

    public async Task<IActionResult> Register(RegisterFormModel formModel)
    {
        DateTime userBirthdate;

        bool isBirthdateValid = DateTime.TryParse(formModel.Birthdate, out userBirthdate);

        bool isBirthdateCorrect = userBirthdate <= DateTime.Now.AddYears(-MinimumAgeRequirement);

        if (!ModelState.IsValid || !isBirthdateValid || !isBirthdateCorrect)
        {
            TempData[ErrorNotification] = InvalidInputDataErrorMessage;

            return RedirectToAction("Register");
        }

        formModel.ParsedBirthdate = userBirthdate;

        (IdentityResult Result, string UserId) userResult = await accountService.CreateAccountAsync(formModel);

        try
        {
            if (userResult.Result.Succeeded)
            {
                TempData[ErrorNotification] = SuccessfulRegistrationMessage;

                await accountService.AddRoleToAccountAsync(userResult.UserId, UserRoleName);

                await accountService.SignInAccountAsync(userResult.UserId);

                if (formModel.Utilities.ReturnUrl == null)
                    return RedirectToAction("Index", "Home");

                return LocalRedirect(formModel.Utilities.ReturnUrl);
            }
            else
            {
                foreach (var error in userResult.Result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                TempData[ErrorNotification] = FailedRegistrationErrorMessage;

                return RedirectToAction("Register");
            }
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
    [AllowAnonymous]

    public async Task<IActionResult> Login(string? returnUrl)
    {
        LoginFormModel loginFormModel = await accountService.GetLoginModelAsync(returnUrl);

        return View(loginFormModel);
    }

    [HttpPost]
    [AllowAnonymous]

    public async Task<IActionResult> Login(LoginFormModel formModel)
    {
        if (ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, InvalidLoginAttemptErrorMessage);

            TempData[ErrorNotification] = InvalidInputDataErrorMessage;

            return RedirectToAction("Login");
        }

        try
        {
            var userResult = await accountService.LoginAccountAsync(formModel);

            if (userResult.Result.Succeeded)
            {
                if (await accountService.IsInRoleAsync(userResult.UserId, "Admin"))
                    return RedirectToAction("SelectArea", "Home");

                if (formModel.Utilities.ReturnUrl == null)
                    return RedirectToAction("Index", "Home");

                TempData[SuccessNotification] = SuccessfulLoginMessage;

                return LocalRedirect(formModel.Utilities.ReturnUrl);
            }

            if (userResult.Result.RequiresTwoFactor)
                return RedirectToAction("LoginWith2fa", new { ReturnUrl = formModel.Utilities.ReturnUrl, RememberMe = formModel.RememberMe });

            if (userResult.Result.IsLockedOut)
            {
                TempData[ErrorNotification] = AccountLockoutErrorMessage;

                return RedirectToAction("Lockout");
            }

            else
            {
                ModelState.AddModelError("Login", IncorrectLoginCredentialErrorMessage);

                TempData[ErrorNotification] = FailedLoginErrorMessage;

                return RedirectToAction("Login");
            }
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


