using PresentationCreatorAPI.Application.Interfaces;

namespace PresentationCreator.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController(INotificationService notificationService) : ControllerBase
{
    private readonly INotificationService _notificationService = notificationService;

    [HttpPost("new")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAsync([FromForm]AddNotificationDto dto)
    {
        var senderId = int.Parse(HttpContext.User.FindFirst("Id")!.Value);
        var total = await _notificationService.CreateAsync(senderId, dto);
        return Ok(total);
    }

    [HttpPost("toAdmin")]
    [Authorize]
    public async Task<IActionResult> SendToAdminAsync(string message)
    {
        var senderId = int.Parse(HttpContext.User.FindFirst("Id")!.Value);
        return Ok(await _notificationService.SendMessageToAdmin(senderId, message));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var userId = int.Parse(HttpContext.User.FindFirst("Id")!.Value);
        return Ok(await _notificationService.GetByIdAsync(userId, id));
    }

    [HttpGet("notifications")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _notificationService.GetAllAsync());
    }

    [HttpGet("user")]
    [Authorize]
    public async Task<IActionResult> GetUserAsync()
    {
        var userId = int.Parse(HttpContext.User.FindFirst("Id")!.Value);
        return Ok(await _notificationService.GetByUserIdAsync(userId));
    }

    [HttpPut("update")]
    [Authorize]
    public async Task<IActionResult> UpdateAsync([FromQuery]int notificationId, [FromForm] UpdateNotificationDto dto)
    {
        await _notificationService.UpdateAsync(notificationId, dto);
        return Ok();
    }

    [HttpDelete("delete")]
    [Authorize]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _notificationService.DeleteAsync(id);
        return Ok();
    }
}
