using Microsoft.AspNetCore.Mvc;
using PresentationCreatorAPI.Application.Common.Exceptions;
using PresentationCreatorAPI.Application.DTOs.UserDtos;
using PresentationCreatorAPI.Application.Interfaces;

namespace PresentationCreator.MVC.Areas.Admin.Controllers;

[Area("admin")]
public class AccountController(IAccountService authService,
    IUserService userService)
    : Controller
{
    private readonly IAccountService _accountService = authService;
    private readonly IUserService _userService = userService;

    public IActionResult Login()
    {
        LoginDto dto = new();
        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        //var user = await _userService.GetByPhoneNumberAsync(dto.PhoneNumber);
        dto.PhoneNumber = "+998997979898";
        var user = await _userService.GetByPhoneNumberAsync(dto.PhoneNumber);
        if(user.Role == "User")
            ModelState.AddModelError(string.Empty, "No acsess for your account.");
        try
        {
            using (var httpClient = new HttpClient())
            {
                // Create key-value pairs for form data
                var formData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("PhoneNumber", dto.PhoneNumber),
                    new KeyValuePair<string, string>("Email", dto.Email),
                    new KeyValuePair<string, string>("Password", dto.Password)
                };
                HttpContent content = new FormUrlEncodedContent(formData);

                using (var response = await httpClient.PostAsync("https://localhost:7042/api/accounts/login", content))
                {
                    response.EnsureSuccessStatusCode();
                    string token = await response.Content.ReadAsStringAsync();

                    if (token.Contains("Invalid credentials"))
                    {
                        ViewBag.Message = "Noto'g'ri telefon raqam yoki parol kiritildi!";
                        return View(dto);
                    }

                    HttpContext.Session.SetString("JWToken", token);
                }
            }
            return RedirectToAction("index", "home");
        }
        catch (StatusCodeException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }catch (HttpRequestException ex)
        {
            ModelState.AddModelError(string.Empty, "Server bilan bog'lanishda xatolik yuz berdi.");
        }

        return View(dto);
        
    }

    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("JWToken");
        return RedirectToAction("Login");
    }
}