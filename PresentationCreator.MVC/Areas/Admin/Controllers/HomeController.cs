using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PresentationCreator.MVC.Areas.Admin.Models;
using PresentationCreatorAPI.Application.Interfaces;
using PresentationCreatorAPI.Application.Services;
using PresentationCreatorAPI.Domain.Entites;
using PresentationCreatorAPI.Enums;
using System.Globalization;
using System.Net.Http.Headers;

namespace PresentationCreator.MVC.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HomeController(
                    IPaymentService paymentService,
                    IUserService userService,
                    IPresentationServise presentationServise) : Controller
    {
        private readonly IPaymentService _paymentService = paymentService;
        private readonly IUserService _userService = userService;
        private readonly IPresentationServise _presentationServise = presentationServise;

        public async Task<IActionResult> Index()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");

            if (string.IsNullOrEmpty(accessToken))
            {
                return RedirectToAction("Login", "Account");
            }

            var url = "https://localhost:7042/api/payment/payments";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            string jsonStr = await client.GetStringAsync(url);

            var response = JsonConvert.DeserializeObject<List<Payment>>(jsonStr).ToList();

            var res2 = HttpContext.User;

            var payments = await _paymentService.GetAllAsync();
            var users = await _userService.GetAllWithoutpagination();
            var presentations = await _presentationServise.GetAllAsync();
            var totalPayments = payments.Where(u => u.Status == PaymentStatus.Accepted.ToString()) // Filtro qilish
                                        .Sum(u => u.Summa);

            var tzTashkent = TimeZoneInfo.FindSystemTimeZoneById("Asia/Tashkent");
            var tashkentTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzTashkent);
            var adjustedTime = tashkentTime.AddHours(-5);
            var todayPayments = payments.Where(u => u.Status == PaymentStatus.Accepted.ToString()) // Filtro qilish
                                        .Where(p => DateTime.Parse(p.CreatedAt).Day == adjustedTime.Day && DateTime.Parse(p.CreatedAt).Month == adjustedTime.Month && DateTime.Parse(p.CreatedAt).Year == adjustedTime.Year)
                                        .Sum(u => u.Summa);
            
            var todayUsers = users.Where(p => DateTime.Parse(p.CreatedAt).Day == adjustedTime.Day && DateTime.Parse(p.CreatedAt).Month == adjustedTime.Month && DateTime.Parse(p.CreatedAt).Year == adjustedTime.Year)
                                  .Count();

            var todayPresentations = presentations.Where(p => DateTime.Parse(p.CreatedAt).Day == adjustedTime.Day && DateTime.Parse(p.CreatedAt).Month == adjustedTime.Month && DateTime.Parse(p.CreatedAt).Year == adjustedTime.Year)
                                  .Count();

            // Statistics gathering
            var lastSixMonths = Enumerable.Range(0, 6)
                .Select(i => DateTime.Now.AddMonths(-i))
                .Select(d => new { Month = d.Month, Year = d.Year })
                .Reverse()
                .ToList();

            var stats = lastSixMonths.Select(month =>
            {
                var monthlyPresentations = presentations.Where(p =>
                {
                    DateTime presentationDate;
                    bool isParsed = DateTime.TryParseExact(p.CreatedAt, "dd-MM-yyyy HH:mm",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out presentationDate);

                    return isParsed && presentationDate.Month == month.Month && presentationDate.Year == month.Year;
                });

                return new MonthlyStats
                {
                    Month = $"{month.Month}/{month.Year}",
                    LanguageCount = monthlyPresentations.Count(p => !string.IsNullOrEmpty(p.Language)),
                    TemplateCount = monthlyPresentations.Count(p => p.Template != 0),
                    PageCount = monthlyPresentations.Sum(p => p.PageCount)
                };
            }).ToList();


            IndexViewModel viewModel = new()
            {
                Payments = payments,
                Users = users,
                Presentations = presentations,
                TotalPayments = totalPayments,
                TodayTotalPayments = todayPayments,
                TodayUsers = todayUsers,
                TodayPresentations = todayPresentations,
                Stats = stats
            };

            return View(viewModel);
        }

        public IActionResult Error(string? url)
        {
            if (url == null)
            {
                url = "/";
            }
            return View("Error404", url);
        }
    }
}
