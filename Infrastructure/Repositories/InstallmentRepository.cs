using Core.Entities;
using Core.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public class InstallmentRepository : IInstallmentRepository
{
    public Task AddRange(List<Installment> installments)
    {
        throw new NotImplementedException();
    }

    public Task<List<Installment>> GetByApprovedLoanId(int Id)
    {
        throw new NotImplementedException();
    }
}
