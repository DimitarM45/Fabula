namespace Fabula.Core.Services;

using Contracts;
using Data.Models;
using Web.ViewModels.Account;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;

using System.Linq;
using System.Threading.Tasks;

public class AccountService : IAccountService
{
    private readonly SignInManager<ApplicationUser> signInManager;

    private readonly UserManager<ApplicationUser> userManager;

    private readonly IUserStore<ApplicationUser> userStore;

    private readonly IUserEmailStore<ApplicationUser> emailStore;

    private readonly IHttpContextAccessor httpContextAccessor;

    public AccountService(
        UserManager<ApplicationUser> userManager,
        IUserStore<ApplicationUser> userStore,
        SignInManager<ApplicationUser> signInManager,
        IHttpContextAccessor httpContextAccessor)
    {
        this.userManager = userManager;
        this.userStore = userStore;
        this.emailStore = GetEmailStore();
        this.signInManager = signInManager;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<RegisterFormModel> GetRegisterModelAsync(string? returnUrl = null)
    {
        RegisterFormModel registerFormModel = new RegisterFormModel()
        {
            Utilities = new AuthUtilitiesViewModel()
        };

        registerFormModel.Utilities.ReturnUrl = returnUrl;

        registerFormModel.Utilities.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        return registerFormModel;
    }

    // Purposefully written to return a tuple in order to separate account creation logic from account sign-in logic

    public async Task<(IdentityResult Result, string UserId)> CreateAccountAsync(RegisterFormModel formModel)
    {
        ApplicationUser user = CreateUser();

        await userStore.SetUserNameAsync(user, formModel.Username, CancellationToken.None);

        await emailStore.SetEmailAsync(user, formModel.Email, CancellationToken.None);

        user.FirstName = formModel.FirstName;

        user.LastName = formModel.LastName;

        IdentityResult result = await userManager.CreateAsync(user, formModel.Password);

        return (result, user.Id.ToString());
    }

    public async Task AddRoleToAccountAsync(string userId, string role)
    {
        ApplicationUser user = await userManager.FindByIdAsync(userId);

        await userManager.AddToRoleAsync(user, role);
    }

    public async Task SignInAccountAsync(string userId)
    {
        ApplicationUser user = await userManager.FindByIdAsync(userId);

        await signInManager.SignInAsync(user, isPersistent: false);
    }

    public async Task<LoginFormModel> GetLoginModelAsync(string? returnUrl = null)
    {
        LoginFormModel loginFormModel = new LoginFormModel();

        loginFormModel.Utilities.ReturnUrl = returnUrl;

        await httpContextAccessor.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        loginFormModel.Utilities.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        return loginFormModel;
    }

    public async Task<(SignInResult Result, string UserId)> LoginAccountAsync(LoginFormModel formModel)
    {
        ApplicationUser user = await signInManager.UserManager.FindByEmailAsync(formModel.LoginCredential) ??
                               await signInManager.UserManager.FindByNameAsync(formModel.LoginCredential);

        SignInResult result = await signInManager.PasswordSignInAsync(user, formModel.Password, formModel.RememberMe, lockoutOnFailure: true);

        (SignInResult Result, string UserId) userResult = (result, user.Id.ToString());

        return userResult;
    }

    private ApplicationUser CreateUser()
        => Activator.CreateInstance<ApplicationUser>();

    private IUserEmailStore<ApplicationUser> GetEmailStore()
        => (IUserEmailStore<ApplicationUser>)userStore;

    public async Task<bool> IsInRoleAsync(string userId, string roleName)
    {
        ApplicationUser user = await userManager.FindByIdAsync(userId);

        bool isInRole = false;

        if (user != null)
            isInRole = await userManager.IsInRoleAsync(user, roleName);

        return isInRole;
    }
}
