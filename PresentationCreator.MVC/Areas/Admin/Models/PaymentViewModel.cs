using PresentationCreatorAPI.Application.DTOs;
using PresentationCreatorAPI.Application.DTOs.UserDtos;

namespace PresentationCreator.MVC.Areas.Admin.Models;

public class PaymentViewModel
{
    public IEnumerable<PaymentDto> Payments { get; set; } = null!;
    public PaymentDto Payment { get; set; } = null!;
    public UserDto User { get; set; } = null!;
}
