using Application.DTOs;

namespace PresentationCreator.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController(IPaymentService paymentService) : ControllerBase
{
    private readonly IPaymentService _paymentService = paymentService;

    [HttpPost("add")]
    [Authorize]
    public async Task<IActionResult> CreateAsync([FromForm] AddPaymentDto dto)
    {
        var userId = int.Parse(HttpContext.User.FindFirst("Id")!.Value);
        await _paymentService.CreateAsync(userId, dto);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        return Ok(await _paymentService.GetByIdAsync(id));
    }

    [HttpGet("payments")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _paymentService.GetAllAsync());
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateAsync([FromForm] UpdatePaymentDto dto)
    {
        var id = int.Parse(HttpContext.User.FindFirst("Id")!.Value);

        await _paymentService.UpdateAsync(id, dto);
        return Ok();
    }

    [HttpDelete("id")]
    [Authorize]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _paymentService.DeleteAsync(id);
        return Ok();
    }
}
