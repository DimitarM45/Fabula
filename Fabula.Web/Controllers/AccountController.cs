namespace Fabula.Web.Controllers;

using Core.Contracts;
using ViewModels.Account;
using Infrastructure.Filters; 

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

public class AccountController : BaseController
{
    private IAccountService accountService;

    public AccountController(IAccountService accountService)
    {
        this.accountService = accountService;
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
    [ServiceFilter(typeof(HtmlSanitizerFilter))]

    public async Task<IActionResult> Register(RegisterFormModel formModel)
    {
        DateTime userBirthdate;

        bool isBirthdateValid = DateTime.TryParse(formModel.Birthdate, out userBirthdate);

        bool isBirthdateCorrect = userBirthdate < DateTime.Now.AddYears(-3);

        if (ModelState.IsValid && isBirthdateValid && isBirthdateCorrect)
        {
            formModel.ParsedBirthdate = userBirthdate;

            (IdentityResult Result, string UserId) userResult = await accountService.CreateAccountAsync(formModel);

            if (userResult.Result.Succeeded)
            {
                await accountService.SignInAccountAsync(userResult.UserId);

                if (formModel.Utilities.ReturnUrl == null)
                    return RedirectToAction("Index", "Home");

                return LocalRedirect(formModel.Utilities.ReturnUrl);
            }

            foreach (var error in userResult.Result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return RedirectToAction("Register");
    }

    [HttpGet]
    [AllowAnonymous]
    [ServiceFilter(typeof(HtmlSanitizerFilter))]

    public async Task<IActionResult> Login(string? returnUrl)
    {
        LoginFormModel loginFormModel = await accountService.GetLoginModelAsync(returnUrl);

        return View(loginFormModel);
    }

    [HttpPost]
    [AllowAnonymous]
    [ServiceFilter(typeof(HtmlSanitizerFilter))]

    public async Task<IActionResult> Login(LoginFormModel formModel)
    {
        if (ModelState.IsValid)
        {
            var result = await accountService.LoginAccountAsync(formModel);

            if (result.Succeeded)
            {
                if (formModel.Utilities.ReturnUrl == null)
                    return RedirectToAction("Index", "Home");

                return LocalRedirect(formModel.Utilities.ReturnUrl);
            }

            if (result.RequiresTwoFactor)
                return RedirectToAction("LoginWith2fa", new { ReturnUrl = formModel.Utilities.ReturnUrl, RememberMe = formModel.RememberMe });

            if (result.IsLockedOut)
                return RedirectToAction("Lockout");

            else
            {
                ModelState.AddModelError("Login", "Wrong password or username/email!");

                return RedirectToAction("Login");
            }
        }

        ModelState.AddModelError("Login", "Invalid login attempt!");

        return RedirectToAction("Login");
    }

    [HttpGet]

    public async Task<IActionResult> ManageAccount(string userId)
    {
        return View();
    }

    [HttpPost]
    [ServiceFilter(typeof(HtmlSanitizerFilter))]

    public async Task<IActionResult> ManageAccount(object model)
    {
        return View();
    }
}


