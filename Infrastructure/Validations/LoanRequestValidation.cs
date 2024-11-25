using Core.DTOs.Request;
using FluentValidation;

namespace Infrastructure.Validations;

public class LoanRequestValidation : AbstractValidator<LoanRequestDTO>
{
    public LoanRequestValidation()
    {
        RuleFor(x => x.LoanType)
           .NotEmpty()
           .WithMessage("Loan Type is required.")
           .MaximumLength(50).WithMessage("Loan Type cannot exceed 50 characters.");

        RuleFor(x => x.TermInMonths)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Term must be greater than 0.");

        RuleFor(x => x.Amount)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0.");
    }
}
