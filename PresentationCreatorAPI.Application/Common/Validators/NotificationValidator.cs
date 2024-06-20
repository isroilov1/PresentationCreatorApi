using FluentValidation;
using PresentationCreatorAPI.Entites;

namespace PresentationCreatorAPI.Application.Common.Validators;

public class NotificationValidator : AbstractValidator<Notification>
{
    public NotificationValidator()
    {
        RuleFor(x => x.Message)
               .NotEmpty()
               .WithMessage("Xabar bo'sh bolmasligi kerak")
               .MinimumLength(3)
               .WithMessage("Xabar minimum 3 ta belgidan iborat bo'lishi kerak")
               .MaximumLength(4000)
               .WithMessage("Xabar maximum 4000 ta belgidan iborat bo'lishi kerak");
    }
}
