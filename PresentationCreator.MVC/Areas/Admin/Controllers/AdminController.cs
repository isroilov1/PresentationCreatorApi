using Microsoft.AspNetCore.Mvc;
using PresentationCreatorAPI.Application.Common.Exceptions;
using PresentationCreatorAPI.Application.DTOs.UserDtos;
using PresentationCreatorAPI.Application.Interfaces;

namespace PresentationCreator.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminController(IAdminService adminService) : Controller
{
    private readonly IAdminService _adminService = adminService;

    [HttpGet]
    public IActionResult ChangeRole()
    {
        UserDto dto = new();
        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> ChangeRole(UserDto dto)
    {

        var token = HttpContext.Session.GetString("JWToken");

        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        try
        {
            // Role changing logic here
            // await _adminService.ChangeRoleAsync(id);
            await _adminService.ChangeUserRoleAsync(dto.Id);

            return RedirectToAction("Index");
        }
        catch (StatusCodeException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return RedirectToAction("index", "users");
        }
    }
}
