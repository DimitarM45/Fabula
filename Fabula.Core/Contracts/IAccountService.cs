namespace Fabula.Core.Contracts;

using Web.ViewModels.Account;

using Microsoft.AspNetCore.Identity;

public interface IAccountService
{
    public Task<RegisterFormModel> GetRegisterModelAsync(string? returnUrl = null);

    public Task<LoginFormModel> GetLoginModelAsync(string? returnUrl = null);

    public Task<(IdentityResult Result, string UserId)> CreateAccountAsync(RegisterFormModel formModel);

    public Task SignInAccountAsync(string userId);

    public Task<SignInResult> LoginAccountAsync(LoginFormModel formModel);
}
