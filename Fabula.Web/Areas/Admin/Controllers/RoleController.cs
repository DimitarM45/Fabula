namespace Fabula.Web.Areas.Admin.Controllers;

using Core.Contracts;
using ViewModels.Admin.Role;

using Microsoft.AspNetCore.Mvc;

public class RoleController : BaseController
{
    public readonly IRoleService roleService;

    public RoleController(IRoleService roleService)
    {
        this.roleService = roleService;
    }

    [HttpGet]

    public async Task<IActionResult> All()
    {
        try
        {
            RoleAllViewModel roleAllViewModel = await roleService.GetAllAsync();

            return View(roleAllViewModel);
        }
        catch (Exception e)
        {
            return RedirectToAction();
        }
    }

    [HttpPost]

    public async Task<IActionResult> Create(RoleFormModel roleFormModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Invalid input data!");

            return RedirectToAction();
        }

        try
        {
            await roleService.AddAsync(roleFormModel);

            return RedirectToAction("All");
        }
        catch (Exception e)
        {
            return RedirectToAction();
        }
    }

    [HttpGet]

    public async Task<IActionResult> Edit(string roleId)
    {
        try
        {
            RoleFormModel? roleFormModel = await roleService.GetForEditAsync(roleId);

            if (roleFormModel == null)
            {
                return RedirectToAction();
            }

            return View(roleFormModel);
        }
        catch (Exception)
        {
            return RedirectToAction();
        }
    }

    [HttpPost]

    public async Task<IActionResult> Edit(RoleFormModel roleFormModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Invalid input data!");

            return View(roleFormModel);
        }

        try
        {
            await roleService.UpdateAsync(roleFormModel);

            return RedirectToAction("All");
        }
        catch (Exception e)
        {
            return RedirectToAction();
        }
    }

    [HttpPost]

    public async Task<IActionResult> Delete(string roleId)
    {
        try
        {
            await roleService.DeleteAsync(roleId);

            return RedirectToAction("All");
        }
        catch (Exception e)
        {
            return RedirectToAction();
        }
    }
}
