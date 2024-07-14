using Microsoft.AspNetCore.Mvc;
using PresentationCreatorAPI.Application.Common.Exceptions;
using PresentationCreatorAPI.Application.DTOs;
using PresentationCreatorAPI.Application.DTOs.UserDtos;
using PresentationCreatorAPI.Application.Interfaces;
using PresentationCreatorAPI.Enums;
using PaymentViewModel = PresentationCreator.MVC.Areas.Admin.Models.PaymentViewModel;

namespace PresentationCreator.MVC.Areas.Admin.Controllers;

[Area("Admin")]
//[Authorize(AuthenticationSchemes = "Admin")]
public class PaymentController(IPaymentService paymentService) : Controller
{
    private readonly IPaymentService _paymentService = paymentService;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<PaymentDto> paymentList = await _paymentService.GetAllAsync();
        PaymentViewModel viewModel = new()
        {
            Payments = paymentList,
            Payment = new PaymentDto(),
            User = new UserDto()
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AcceptPayment(PaymentViewModel dto)
    {
        try
        {
            //var accepterId = int.Parse(HttpContext.User.FindFirst("Id")!.Value);
            var accepterId = 1;
            var status = PaymentStatus.Accepted;
            var adminCaption = dto.Payment.AdminCaption;
            await _paymentService.AcceptPaymentAsync(dto.Payment.Id, status, adminCaption, accepterId);
            return RedirectToAction("Index");
        }
        catch (StatusCodeException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return RedirectToAction("index", "payment");
        }
        catch (ValidatorException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return RedirectToAction("index", "payment");
        }
    }

    [HttpPost]
    public async Task<IActionResult> RejectPayment(PaymentViewModel dto)
    {
        try
        {
            //var accepterId = int.Parse(HttpContext.User.FindFirst("Id")!.Value);
            var accepterId = 1;
            var status = PaymentStatus.Rejected;
            var adminCaption = dto.Payment.AdminCaption;
            await _paymentService.AcceptPaymentAsync(dto.Payment.Id, status, adminCaption, accepterId);
            return RedirectToAction("Index");
        }
        catch (StatusCodeException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return RedirectToAction("index", "payment");
        }
        catch (ValidatorException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return RedirectToAction("index", "payment");
        }
    }
}
