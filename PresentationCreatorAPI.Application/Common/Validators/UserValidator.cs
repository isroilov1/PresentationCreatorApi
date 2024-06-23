using FluentValidation;
using PresentationCreatorAPI.Domain.Entites;

namespace PresentationCreatorAPI.Application.Common.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("F.I.O bo'sh bo'lmasligi lozim")
            .MinimumLength(3)
            .WithMessage("F.I.O kamida 3ta belgidan iborat bo'lishi kerak")
            .MaximumLength(100)
            .WithMessage("F.I.O maximum 100ta belgidan oshmasligi kerak"); ;
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Telefon raqam bo'sh bolmasligi lozim")
            .MinimumLength(3)
            .WithMessage("Telefon raqamlari soni kamida 3ta belgidan iborat bo'lishi kerak")
            .MaximumLength(50)
            .WithMessage("Telefon raqamlari soni maximum 100ta belgidan oshmasligi kerak");
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email bo'sh bolmasligi lozim")
            .EmailAddress()
            .WithMessage("Emailni to'g'ri formatda kiriting")
            .MinimumLength(3)
            .WithMessage("Email kamida 3ta belgidan iborat bo'lishi kerak")
            .MaximumLength(100)
            .WithMessage("Email maximum 100ta belgidan oshmasligi kerak");
        RuleFor(x => x.Password)
             .NotEmpty()
             .WithMessage("Password bo'sh bolmasligi lozim")
             .MinimumLength(6)
             .WithMessage("Password kamida 6 xona bolishi kerak")
             .MaximumLength(50)
             .WithMessage("Password maximum 50 xona bolishi kerak");
    }
}
