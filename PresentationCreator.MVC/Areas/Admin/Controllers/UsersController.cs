using Microsoft.AspNetCore.Mvc;
using PresentationCreator.MVC.Areas.Admin.Models;
using PresentationCreatorAPI.Application.Common.Exceptions;
using PresentationCreatorAPI.Application.DTOs;
using PresentationCreatorAPI.Application.DTOs.UserDtos;
using PresentationCreatorAPI.Application.Interfaces;
using PresentationCreatorAPI.Application.Services;
using PresentationCreatorAPI.Enums;

namespace PresentationCreator.MVC.Areas.Admin.Controllers
{

    [Area("Admin")]
    //[Authorize(AuthenticationSchemes = "Admin")]
    public class UsersController(IUserService userService,
        IAdminService adminService,
        IPaymentService paymentService,
        IPresentationServise presentationServise) : Controller
    {
        private readonly IUserService _userService = userService;
        private readonly IAdminService _adminService = adminService;
        private readonly IPaymentService _paymentService = paymentService;
        private readonly IPresentationServise _presentationServise = presentationServise;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userDto = await _userService.GetAllWithoutpagination();
            AddNotificationDto notification = new();
            var usersViewModel = new UsersViewModel
            {
                Users = userDto,
                Notification = notification
            };
            return View(usersViewModel);
        }

        [HttpGet]
        public IActionResult Updatebalance()
        {
            UpdateUserBalanceDto dto = new();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBalance(UpdateUserBalanceDto dto)
        {
            try
            {
                await _adminService.UpdateBalanceAsync(dto);
                return RedirectToAction("Index"); // You can redirect to another action if needed
            }
            catch (StatusCodeException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto); // Return the view with the current dto and error message
            }
        }

        [HttpGet]
        public async Task<IActionResult> SingleLineChart()
        {
            var payments = await _paymentService.GetAllAsync();
            var last10Days = DateTime.Now.AddDays(-9); // So'nggi 10 kunni hisoblash
            var groupedPayments = payments
                .Where(p => p.Status == PaymentStatus.Accepted.ToString())
                .Where(p => DateTime.ParseExact(p.CreatedAt, "MM-dd-yyyy HH:mm", null) >= last10Days)
                .GroupBy(p => DateTime.ParseExact(p.CreatedAt, "MM-dd-yyyy HH:mm", null).Date)
                .Select(g => new
                {
                    Date = g.Key,
                    Sum = g.Sum(p => p.Summa)
                })
                .ToList();

            return Json(groupedPayments);
        }

        [HttpGet]
        public async Task<IActionResult> PresentationsChart()
        {
            var presentations = await _presentationServise.GetAllAsync();
            var languages = presentations.GroupBy(p => p.Language)
                                            .Select(g => new { Language = g.Key, Count = g.Count() })
                                            .ToList();

            return Json(languages);
        }
    }
}
