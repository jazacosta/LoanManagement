using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface ILoanRequestRepository
{
    Task<Request> AddLoanRequest(Request request);
}
