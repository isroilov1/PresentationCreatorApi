using PresentationCreatorAPI.Application.DTOs.PresentationDtos;
using PresentationCreatorAPI.Application.Interfaces;
using PresentationCreatorAPI.Application.Services;

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
}
