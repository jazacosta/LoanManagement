using Core.DTOs.LoanManagement;

namespace Core.Interfaces.Services;

public interface ILoanService
{
    Task ApproveLoan(int Id);
    Task RejectLoan(int Id, string RejectionReason);
    Task<DetailedLoanDTO> GetLoanDetails(int approvedLoanId, CancellationToken cancellationToken = default);
}
