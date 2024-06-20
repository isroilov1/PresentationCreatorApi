using PresentationCreatorAPI.Application.Interfaces;

namespace PresentationCreator.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController(IPaymentService paymentService) : ControllerBase
{
    private readonly IPaymentService _paymentService = paymentService;

    [HttpPost("new")]
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

    [HttpGet("user")]
    [Authorize]
    public async Task<IActionResult> GetUserAsync()
    {
        var userId = int.Parse(HttpContext.User.FindFirst("Id")!.Value);
        return Ok(await _paymentService.GetByUserIdAsync(userId));
    }

    [HttpGet("byPhone")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserByPhoneAsync([FromForm]string phone)
    {
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

    [HttpDelete("delete")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _paymentService.DeleteAsync(id);
        return Ok();
    }
}
