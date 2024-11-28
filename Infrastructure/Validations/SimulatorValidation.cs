using Core.DTOs.InstallmentSimulator;
using FluentValidation;

namespace Infrastructure.Validations;

public class SimulatorValidation : AbstractValidator<InstallmentSimDTO>
{
    public SimulatorValidation()
    {
        RuleFor(x => x.Amount)
            .NotEmpty()
            .GreaterThanOrEqualTo(1000000)
            .WithMessage("The amount must be greater than 1000000 and it cannot be empty.");

        RuleFor(x => x.TermInMonths)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("The term must be greater than 0.");
    }
}
