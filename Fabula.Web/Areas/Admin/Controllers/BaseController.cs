namespace Fabula.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Area("Admin")]
[Authorize(Roles = "Admin")]
[AutoValidateAntiforgeryToken]

public class BaseController : Controller
{

}
