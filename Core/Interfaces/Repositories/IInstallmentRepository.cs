using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IInstallmentRepository
{
    Task AddRange(List<Installment> installments);
    Task<List<Installment>> GetByApprovedLoanId(int Id);
}
