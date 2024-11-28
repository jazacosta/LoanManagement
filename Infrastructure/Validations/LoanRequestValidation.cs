using Core.DTOs.Request;
using FluentValidation;

namespace Infrastructure.Validations;

public class LoanRequestValidation : AbstractValidator<LoanRequestDTO>
{
    public LoanRequestValidation()
    {
        RuleFor(x => x.CustomerId)
           .NotNull()
           .GreaterThan(0)
           .WithMessage("CustomerId is required.");

        RuleFor(x => x.LoanType)
           .NotEmpty()
           .Matches(@"^[a-zA-Z0-9\s]*$")
           .MaximumLength(50)
           .WithMessage("Loan Type must only contain letters, numbers or spaces and cannot exceed 50 characters.");

        RuleFor(x => x.TermInMonths)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Term must be greater than 0.");

        RuleFor(x => x.Amount)
            .NotEmpty()
            .GreaterThanOrEqualTo(1000000)
            .WithMessage("Amount must be greater than 1000000 and it cannot be empty.");
    }
}
