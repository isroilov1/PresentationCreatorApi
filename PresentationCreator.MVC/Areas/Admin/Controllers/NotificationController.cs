using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PresentationCreator.MVC.Areas.Admin.Models;
using PresentationCreatorAPI.Application.Common.Exceptions;
using PresentationCreatorAPI.Application.DTOs;
using PresentationCreatorAPI.Application.Interfaces;

namespace PresentationCreator.MVC.Areas.Admin.Controllers;

[Area("Admin")]
//[Authorize(AuthenticationSchemes = "Admin")]
public class NotificationController(INotificationService notificationService) : Controller
{
    private readonly INotificationService _notificationService = notificationService;

    [HttpGet]
    public async Task<IActionResult> Sendtouser()
    {
        await _notificationService.GetAllAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Sendtouser(UsersViewModel dto)
    {
        try
        {
            var senderId = 1;
            var notification = new AddNotificationDto
            {
                Message = dto.Notification.Message,
                RecipientIds = dto.Notification.RecipientIds
            };
            var total = await _notificationService.CreateAsync(senderId, dto.Notification);
        }
        catch (StatusCodeException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }
        catch (ValidationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }
        return RedirectToAction("index", "users");
    }
}
