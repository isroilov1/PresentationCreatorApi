using FluentValidation;
using PresentationCreatorAPI.Domain.Entites;

namespace PresentationCreatorAPI.Application.Common.Validators;

public class PresentationValidator : AbstractValidator<Presentation>
{
    public PresentationValidator()
    {
        RuleFor(x => x.Theme)
            .NotEmpty()
            .WithMessage("Mavzu bo'sh bolmasligi kerak")
            .MinimumLength(3)
            .WithMessage("Mavzu minimum 3 ta belgidan iborat bo'lishi kerak")
            .MaximumLength(400)
            .WithMessage("Mavzu maximum 400 ta belgidan iborat bo'lishi kerak");

        RuleFor(x => x.Author)
            .NotEmpty()
            .WithMessage("Muallif ism-familiyasi bo'sh bolmasligi kerak")
            .MinimumLength(3)
            .WithMessage("Muallif ism-familiyasi minimum 3 ta belgidan iborat bo'lishi kerak")
            .MaximumLength(400)
            .WithMessage("Muallif ism-familiyasi maximum 400 ta belgidan iborat bo'lishi kerak");

        RuleFor(x => x.PageCount)
            .NotNull()
            .WithMessage("Sahifalar soni bo'sh bolmasligi kerak")
            .GreaterThanOrEqualTo((byte)5)
            .WithMessage("Sahifalar soni kamida 5ta bo'lishi kerak")
            .LessThanOrEqualTo((byte)20)
            .WithMessage("Sahifalar soni maximum 20ta bo'lishi kerak");

        RuleFor(x => x.Template)
            .NotEmpty()
            .WithMessage("Template bo'sh bolmasligi kerak");

        RuleFor(x => x.Language)
            .NotEmpty()
            .WithMessage("Til bo'sh bolmasligi kerak");
    }
}
