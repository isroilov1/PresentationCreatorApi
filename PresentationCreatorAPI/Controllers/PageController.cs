using PresentationCreatorAPI.Application.Interfaces;

namespace PresentationCreatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController(IPageService pageService) : ControllerBase
    {
        private readonly IPageService _pageService = pageService;

        [HttpGet("pages")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _pageService.GetAllAsync());
        }
    }
}
