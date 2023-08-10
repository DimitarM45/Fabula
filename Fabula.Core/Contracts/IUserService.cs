namespace Fabula.Core.Contracts;

using Fabula.Web.ViewModels.Admin.User;
using Web.ViewModels.User;

public interface IUserService
{
    string? GetUserId();

    Task<UserProfileViewModel?> GetProfileAsync(string userId);

    Task<int> GetCountAsync();

    Task<IEnumerable<UserDashboardViewModel>> GetAllForAdminDashboardAsync();

    Task<string?> GetUsernameAsync(string userId);
}
