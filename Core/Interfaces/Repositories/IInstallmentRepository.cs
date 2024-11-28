using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IInstallmentRepository
{
    Task AddInstallment(Installment installment);
    Task UpdateInstallments(Installment installment);
    Task<List<Installment>> GetInstallments(int approvedLoanId);
}
