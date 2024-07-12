using PresentationCreatorAPI.Application.DTOs;
using PresentationCreatorAPI.Application.DTOs.UserDtos;

namespace PresentationCreator.MVC.Areas.Admin.Models;

public class IndexViewModel
{
    public List<PaymentDto> Payments { get; set; } = new();
    public List<UserDto> Users { get; set; } = new();
    public List<PresentationCreatorAPI.Application.DTOs.PresentationDtos.PresentationDto> Presentations { get; set; } = new();
    public int TotalPayments { get; set; }
    public int TodayTotalPayments { get; set; }
    public int TodayUsers { get; set; }
    public int TodayPresentations { get; set; }
    public List<MonthlyStats> Stats { get; set; } = new(); // Yangi property qo'shildi
}

public class MonthlyStats
{
    public string Month { get; set; } = string.Empty;
    public int LanguageCount { get; set; }
    public int TemplateCount { get; set; }
    public int PageCount { get; set; }
}