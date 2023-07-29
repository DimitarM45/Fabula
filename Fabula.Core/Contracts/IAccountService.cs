namespace Fabula.Core.Contracts;

using Web.ViewModels.Account;

using Microsoft.AspNetCore.Identity;

public interface IAccountService
{
    Task<RegisterFormModel> GetRegisterModelAsync(string? returnUrl = null);

    Task<LoginFormModel> GetLoginModelAsync(string? returnUrl = null);

    Task<(IdentityResult Result, string UserId)> CreateAccountAsync(RegisterFormModel formModel);

    Task SignInAccountAsync(string userId);

    Task<(SignInResult Result, string UserId)> LoginAccountAsync(LoginFormModel formModel);

    Task AddRoleToAccountAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string roleName);
}
