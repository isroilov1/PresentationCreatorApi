using PresentationCreatorAPI.Application.Interfaces;

namespace MovieNTV.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminsController(IAdminService adminService,
                              IUserService userService,
                              IPaymentService paymentService) : ControllerBase
{
    private readonly IAdminService _adminService = adminService;
    private readonly IUserService _userService = userService;
    private readonly IPaymentService _paymentService = paymentService;

    [HttpPut("changeRole")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ChangeUserRoleAsync(int id)
    {
        await _adminService.ChangeUserRoleAsync(id);
        return Ok();
    }

    [HttpPost("id")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUserAsync(int id, [FromForm]UpdateUserDto dto)
    {
        await _userService.UpdateAsync(id, dto);
        return Ok();
    }

    [HttpGet("admins")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllAdminAsync()
        => Ok(await _adminService.GetAllAdminAsync());

    [HttpPut("payment/accept")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AcceptPaymentAsync([FromForm] int paymentId, [FromForm] string adminCaption)
    {
        var accepterId = int.Parse(HttpContext.User.FindFirst("Id")!.Value);
        var status = PaymentStatus.Accepted;
        await _paymentService.AcceptPaymentAsync(paymentId, status, adminCaption, accepterId);
        return Ok();
    }

    [HttpPut("payment/reject")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RejectPaymentAsync([FromForm] int paymentId, [FromForm] string adminCaption)
    {
        var accepterId = int.Parse(HttpContext.User.FindFirst("Id")!.Value);
        var status = PaymentStatus.Rejected;
        await _paymentService.RejectPaymentAsync(paymentId, status, adminCaption, accepterId);
        return Ok();
    }

    [HttpPut("updateBalance")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateBalanceAsync([FromForm] UpdateUserBalanceDto dto)
    {
        await _adminService.UpdateBalanceAsync(dto);
        return Ok();
    }
}
