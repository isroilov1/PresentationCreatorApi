using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentationCreator.MVC.Areas.Admin.Models;
using PresentationCreatorAPI.Application.DTOs.PresentationDtos;
using PresentationCreatorAPI.Application.Interfaces;
using PresentationCreatorAPI.Domain.Entites;
using System.Net.Http.Headers;

namespace PresentationCreator.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class PresentationController(IPresentationServise presentationServise,
    IHttpClientFactory clientFactory) : Controller
{
    private readonly IPresentationServise _presentationServise = presentationServise;
    private readonly IHttpClientFactory _clientFactory = clientFactory;

    [HttpGet]
    public async Task<IActionResult> Presentation()
    {
        var accessToken = HttpContext.Session.GetString("JWToken");

        if (string.IsNullOrEmpty(accessToken))
        {
            return RedirectToAction("Login", "Account");
        }

        var client = _clientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var url = "https://localhost:5281/api/presentation/presentations";

        HttpResponseMessage response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            // Xato holatini qaytaring yoki boshqa chora ko'ring
            return StatusCode((int)response.StatusCode);
        }

        var jsonStr = await response.Content.ReadAsStringAsync();
        var presentations = JsonConvert.DeserializeObject<List<Presentation>>(jsonStr);
        var presentationsDto = presentations!.Select(u => (PresentationDtoMvc)u).ToList();

        PresentationViewModel viewModel = new()
        {
            Presentations = presentationsDto
        };

        return View(viewModel);
    }
}
