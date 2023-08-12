namespace Fabula.Core.Contracts;

using Web.ViewModels.User;
using Web.ViewModels.Admin.User;

public interface IUserService
{
    string? GetUserId();

    Task<UserProfileViewModel?> GetProfileAsync(string userId);

    Task<int> GetCountAsync();

    Task<IEnumerable<UserDashboardViewModel>> GetAllForAdminDashboardAsync();

    Task<string?> GetUsernameAsync(string userId);

    Task AddRoleToUserAsync(string userId, string roleName);

    Task AddFavoriteGenreToUserAsync(string userId, int genreId);

    Task RemoveFavoriteGenreFromUserAsync(string userId, int genreId);
}
