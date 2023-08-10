namespace Fabula.Web.ViewModels.Admin.Dashboard;

using User;
using Composition;

public class DashboardViewModel
{
    public DashboardViewModel()
    {
        Users = new HashSet<UserDashboardViewModel>();
        Compositions = new HashSet<CompositionDashboardViewModel>();
    }

    public IEnumerable<UserDashboardViewModel> Users { get; set; }

    public IEnumerable<CompositionDashboardViewModel> Compositions { get; set; }
}
