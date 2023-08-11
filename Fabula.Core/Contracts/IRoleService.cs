namespace Fabula.Core.Contracts;

using Web.ViewModels.Admin.Role;

public interface IRoleService
{
    Task<RoleAllViewModel> GetAllAsync();

    Task AddAsync(RoleFormModel roleFormModel);

    Task DeleteAsync(string roleId);

    Task<RoleFormModel?> GetForEditAsync(string roleId);

    Task UpdateAsync(RoleFormModel roleFormModel);
}