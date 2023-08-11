namespace Fabula.Web.ViewModels.Admin.Role;

public class RoleAllViewModel
{
    public RoleFormModel? RoleFormModel { get; set; }

    public IEnumerable<RoleViewModel>? Roles { get; set; }
}
