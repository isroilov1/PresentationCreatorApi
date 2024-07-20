using Microsoft.AspNetCore.Mvc;
using PresentationCreatorAPI.Application.Common.Exceptions;
using PresentationCreatorAPI.Application.DTOs.UserDtos;
using PresentationCreatorAPI.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Authentication;

namespace PresentationCreator.MVC.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public AccountController(IAccountService authService, IUserService userService)
        {
            _accountService = authService;
            _userService = userService;
        }

        public IActionResult Login()
        {
            LoginDto dto = new();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            // For testing purposes, setting a static phone number
            dto.PhoneNumber = "+998997979898";

            var user = await _userService.GetByPhoneNumberAsync(dto.PhoneNumber);
            if (user.Role == "User")
            {
                ModelState.AddModelError(string.Empty, "Sizning hisob qaydnomangiz uchun kirish imkoniyati yo'q.");
                return View(dto);
            }

            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Ensuring support for latest TLS protocols
                    httpClient.DefaultRequestVersion = new Version(2, 0);
                    httpClient.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher;

                    // Create key-value pairs for form data
                    var formData = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("PhoneNumber", dto.PhoneNumber),
                        new KeyValuePair<string, string>("Email", dto.Email),
                        new KeyValuePair<string, string>("Password", dto.Password)
                    };

                    HttpContent content = new FormUrlEncodedContent(formData);

                    using (var response = await httpClient.PostAsync("https://localhost:5281/api/accounts/login", content))
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
            }
            catch (HttpRequestException ex)
            {
                // Log the inner exception details if available
                var innerExceptionMessage = ex.InnerException?.Message ?? "Server bilan bog'lanishda xatolik yuz berdi.";
                ModelState.AddModelError(string.Empty, innerExceptionMessage);
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
}
