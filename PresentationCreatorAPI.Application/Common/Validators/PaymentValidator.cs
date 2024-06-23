using FluentValidation;
using PresentationCreatorAPI.Domain.Entites;

namespace PresentationCreatorAPI.Application.Common.Validators;

public class PaymentValidator : AbstractValidator<Payment>
{
    public PaymentValidator()
    {
        RuleFor(x => x.Summa)
            .NotNull()
            .WithMessage("Summa bo'sh bo'lmasligi kerak")
            .MustAsync((summa, cancellationToken) => BeLessThan500(summa, cancellationToken))
            .WithMessage("Summa 500 dan kam bo'lmasligi kerak!");
        RuleFor(x => x.FilePath)
            .NotNull()
            .WithMessage("File bo'sh bo'lmasligi kerak");
    }
    private Task<bool> BeLessThan500(int summa, CancellationToken cancellationToken)
    {
        return Task.FromResult(summa > 500);
    }
}
