using PresentationCreatorAPI.Application.DTOs.PresentationDtos;
using PresentationCreatorAPI.Application.Interfaces;

namespace PresentationCreatorAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PresentationController(IPresentationServise presentationServise) : ControllerBase
{
    private readonly IPresentationServise _presentationServise = presentationServise;

    [HttpPost("new")]
    [Authorize]
    public async Task<IActionResult> CreateAsync([FromForm] AddPresentationDto dto)
    {
        var userId = int.Parse(HttpContext.User.FindFirst("Id")!.Value);
        await _presentationServise.CreateAsync(dto, userId);
        return Ok();
    }

    [HttpGet("presentations")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _presentationServise.GetAllAsync());
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetAsync(int id)
    {
        return Ok(await _presentationServise.GetByIdAsync(id));
    }

    [HttpGet("user")]
    [Authorize]
    public async Task<IActionResult> GetByUserAsync()
    {
        var userId = int.Parse(HttpContext.User.FindFirst("Id")!.Value);
        return Ok(await _presentationServise.GetByUserAsync(userId));
    }

    [HttpDelete("id")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _presentationServise.DeleteAsync(id);
        return Ok();
    }
}
