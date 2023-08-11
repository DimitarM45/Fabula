namespace Fabula.Core.Services;

using Data;
using Contracts;
using Web.ViewModels.Admin.Role;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using System.Threading.Tasks;

public class RoleService : IRoleService
{
    private readonly FabulaDbContext dbContext;

    private readonly RoleManager<IdentityRole<Guid>> roleManager;

    public RoleService(FabulaDbContext dbContext, 
        RoleManager<IdentityRole<Guid>> roleManager) 
    {
        this.dbContext = dbContext;
        this.roleManager = roleManager;
    }

    public async Task<RoleAllViewModel> GetAllAsync()
    {
        IEnumerable<RoleViewModel> roleViewModels = await dbContext.Roles
            .AsNoTracking()
            .Select(r => new RoleViewModel()
            {
                Id = r.Id.ToString(),
                Name = r.Name
            })
            .ToListAsync();

        foreach (RoleViewModel role in roleViewModels)
            role.Users = await dbContext.UserRoles.CountAsync(r => r.RoleId.ToString() == role.Id);

        RoleAllViewModel roleAllViewModel = new RoleAllViewModel()
        {
            RoleFormModel = new RoleFormModel(),
            Roles = roleViewModels
        };

        return roleAllViewModel;
    }

    public async Task AddAsync(RoleFormModel roleFormModel)
    {
        IdentityRole<Guid> roleToAdd = new IdentityRole<Guid>()
        {
            Name = roleFormModel.Name,
            NormalizedName = roleFormModel.Name.ToUpper(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };

        await roleManager.CreateAsync(roleToAdd);
    }

    public async Task DeleteAsync(string roleId)
    {
        IdentityRole<Guid> roleToDelete = await roleManager.FindByIdAsync(roleId);

        await roleManager.DeleteAsync(roleToDelete);
    }

    public async Task UpdateAsync(RoleFormModel roleFormModel)
    {
        IdentityRole<Guid>? roleToUpdate = await roleManager.FindByIdAsync(roleFormModel.Id);

        if (roleToUpdate == null)
            return;

        roleToUpdate.Name = roleFormModel.Name;
        roleToUpdate.NormalizedName = roleFormModel.Name.ToUpper();

        await roleManager.UpdateAsync(roleToUpdate);
    }

    public async Task<RoleFormModel?> GetForEditAsync(string roleId)
    {
        IdentityRole<Guid>? roleToEdit = await roleManager.FindByIdAsync(roleId);

        if (roleToEdit == null)
            return null;

        RoleFormModel roleFormModel = new RoleFormModel()
        {
            Id = roleToEdit.Id.ToString(),
            Name = roleToEdit.Name
        };

        return roleFormModel;
    }
}
