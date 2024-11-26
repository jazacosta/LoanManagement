using Core.DTOs.LoanManagement;

namespace Core.Interfaces.Services;

public interface ILoanService
{
    Task ApproveLoan(ApprovedLoanDTO loanApproval);
    Task RejectLoan(RejectedLoanDTO loanRejection);
}
