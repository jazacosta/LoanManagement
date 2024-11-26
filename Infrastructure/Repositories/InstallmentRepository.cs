using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public class InstallmentRepository : IInstallmentRepository
{
    private readonly ApplicationDbContext _context;

    public InstallmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task AddInstallment(Installment installment)
    {
        _context.Installments.Add(installment);
        await _context.SaveChangesAsync();
    }

    public Task AddRange(List<Installment> installments)
    {
        throw new NotImplementedException();
    }

    public Task<List<Installment>> GetByApprovedLoanId(int Id)
    {
        throw new NotImplementedException();
    }
}
