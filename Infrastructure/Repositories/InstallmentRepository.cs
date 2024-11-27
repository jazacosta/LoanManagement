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

}
