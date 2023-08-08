namespace Fabula.Core.Contracts;

using Web.ViewModels.User;

public interface IUserService
{
    string? GetUserId();

    Task<UserProfileViewModel?> GetProfileAsync(string userId);

    Task<int> GetCountAsync();
}
