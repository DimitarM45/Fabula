﻿namespace Fabula.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using System.Security.Claims;

[Authorize]
[AutoValidateAntiforgeryToken]

public class BaseController : Controller
{
    protected string GetUserId()
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        return userId;
    }
}
