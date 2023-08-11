namespace Fabula.Web.ViewModels.Admin.Role;

public class RoleAllViewModel
{
    public RoleAllViewModel()
    {
        Roles = new HashSet<RoleViewModel>();
    }

    public RoleFormModel RoleFormModel { get; set; } = null!;

    public IEnumerable<RoleViewModel> Roles { get; set; }
}
