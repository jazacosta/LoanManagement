using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface ILoanRequestRepository
{
    Task<LoanRequest> AddLoanRequest(LoanRequest request);
}
