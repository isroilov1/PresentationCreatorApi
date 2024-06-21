using PresentationCreatorAPI.Application.Common.Utils;
using PresentationCreatorAPI.Application.Interfaces;

namespace PresentationCreator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _userService.GetByIdAsync(id));
        }

        [HttpGet("users")]
        [Authorize]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        {
            return Ok(await _userService.GetAllAsync(@params));
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetUserAsync()
        {
            var id = int.Parse(HttpContext.User.FindFirst("Id")!.Value);
            return Ok(await _userService.GetByIdAsync(id));
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateUserDto dto)
        {
            var id = int.Parse(HttpContext.User.FindFirst("Id")!.Value);

            await _userService.UpdateAsync(id, dto);
            return Ok();
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _userService.DeleteAsync(id);
            return Ok();
        }
    }
}
