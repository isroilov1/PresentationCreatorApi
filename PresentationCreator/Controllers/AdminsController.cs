namespace MovieNTV.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminsController(IAdminService adminService,
                              IUserService userService) : ControllerBase
{
    private readonly IAdminService _adminService = adminService;
    private readonly IUserService _userService = userService;

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

    [HttpPut("updateBalance")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateBalanceAsync([FromForm] UpdateUserBalanceDto dto)
    {
        await _adminService.UpdateBalanceAsync(dto);
        return Ok();
    }
}
