using FluentValidation;
using PresentationCreatorAPI.Domain.Entites;

namespace PresentationCreatorAPI.Application.Common.Validators;

public class PageValidator : AbstractValidator<Page>
{
    public PageValidator()
    {
        RuleFor(x => x.Title)
            .NotNull().WithMessage("Sarlavha bo'sh bo'lmasligi kerak")
            .MinimumLength(3).WithMessage("Sarlavha 3 yoki undan ko'proq belgidan iborat bo'lishi kerak!");
        RuleFor(x => x.Text)
            .NotNull().WithMessage("Matn bo'sh bo'lmasligi kerak")
            .MinimumLength(20).WithMessage("Matn 20 yoki undan ko'proq belgidan iborat bo'lishi kerak!");
    }
}
