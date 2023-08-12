namespace Fabula.Web.Areas.Admin.Controllers;

using static Common.GlobalConstants;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Area(AdminAreaName)]
[Authorize(Roles = AdminRoleName)]
[AutoValidateAntiforgeryToken]

public class BaseController : Controller
{
    protected string GetControllerName()
    {
        string controllerName = ControllerContext.ActionDescriptor.ControllerName;

        return controllerName;
    }

    protected string GetActionName()
    {
        string actionName = ControllerContext.ActionDescriptor.ActionName;

        return actionName;
    }
}
