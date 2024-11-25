using Core.DTOs.InstallmentSimulator;
using FluentValidation;

namespace Infrastructure.Validations;

public class SimulatorValidation : AbstractValidator<InstallmentSimDTO>
{
    public SimulatorValidation()
    {
        RuleFor(x => x.Amount)
            .NotEmpty()
            .GreaterThan(0).WithMessage("The amount must be greater than 0.");

        RuleFor(x => x.Term)
            .NotEmpty()
            .GreaterThan(0).WithMessage("The term must be greater than 0.");
    }
}
