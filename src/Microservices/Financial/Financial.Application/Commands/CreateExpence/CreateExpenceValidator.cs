using FluentValidation;

namespace Financial.Application.Commands.CreateExpence;

public class CreateExpenceValidator : AbstractValidator<CreateExpenceRequest>
{
    public CreateExpenceValidator()
    {
        RuleFor(x => x.Title).NotEmpty().NotNull().Length(3,20);
        RuleFor(x => x.Description).NotEmpty().NotNull().Length(3, 100);
        RuleFor(x => x.TotalAmount).GreaterThan(0);
    }
}
