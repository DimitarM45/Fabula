namespace Fabula.Web.Areas.Admin.Controllers;

using static Common.GlobalConstants;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Area(AdminAreaName)]
[Authorize(Roles = AdminRoleName)]
[AutoValidateAntiforgeryToken]

public class BaseController : Controller
{

}
