using Domain.Enums;

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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAsync(int id)
    {
        return Ok(await _paymentService.GetByIdAsync(id));
    }

    [HttpGet("byPhone")]
    [Authorize]
    public async Task<IActionResult> GetUserAsync([FromForm]string phone)
    {
        var userId = int.Parse(HttpContext.User.FindFirst("Id")!.Value);
        return Ok(await _paymentService.GetByPhoneNumberAsync(phone));
    }

    [HttpGet("payments")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _paymentService.GetAllAsync());
    }

    [HttpPut("update")]
    [Authorize]
    public async Task<IActionResult> UpdateAsync([FromForm] UpdatePaymentDto dto)
    {
        await _paymentService.UpdateAsync(dto);
        return Ok();
    }

    [HttpPut("accept-reject")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AcceptPaymentAsync([FromForm] int paymentId, [FromForm] PaymentStatus status, [FromForm] string adminCaption)
    {
        await _paymentService.AcceptOrRejectAsync(paymentId, status, adminCaption);
        return Ok();
    }

    [HttpDelete("delete")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _paymentService.DeleteAsync(id);
        return Ok();
    }
}
