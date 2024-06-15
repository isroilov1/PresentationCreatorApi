using Application.DTOs.MovieDtos;
using Application.DTOs.NotificationDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationCreator.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController(INotificationService notificationService) : ControllerBase
{
    private readonly INotificationService _notificationService = notificationService;

    [HttpPost("new")]
    [Authorize]
    public async Task<IActionResult> CreateAsync([FromForm]AddNotificationDto dto)
    {
        var senderId = int.Parse(HttpContext.User.FindFirst("Id")!.Value);
        var total = await _notificationService.CreateAsync(senderId, dto);
        return Ok(total);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        return Ok(await _notificationService.GetByIdAsync(id));
    }

    [HttpGet("notifications")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _notificationService.GetAllAsync());
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateAsync([FromForm] UpdateNotificationDto dto)
    {
        var id = int.Parse(HttpContext.User.FindFirst("Id")!.Value);

        await _notificationService.UpdateAsync(id, dto);
        return Ok();
    }

    [HttpDelete("id")]
    [Authorize]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _notificationService.DeleteAsync(id);
        return Ok();
    }

}
