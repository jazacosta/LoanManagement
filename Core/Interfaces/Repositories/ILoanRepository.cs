using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface ILoanRepository
{
    Task<LoanRequest> GetLoanRequestById(int Id);
    Task SaveApprovedLoan(ApprovedLoan approvedLoan);
    Task SaveInstallments(List<Installment> installments);
    Task UpdateLoanRequest(LoanRequest loanRequest);
    Task<TermInterestRate> GetTerm(int termId); 
}
