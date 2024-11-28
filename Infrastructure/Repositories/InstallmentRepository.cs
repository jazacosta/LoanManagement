using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

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

    public async Task UpdateInstallments(Installment installment)
    {
        _context.Installments.UpdateRange(installment);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Installment>> GetInstallments(int approvedLoanId)
    {
        return await _context.Installments
            .Where(x => x.ApprovedLoanId == approvedLoanId)
            .OrderBy(x => x.DueDate)
            .ToListAsync();
    }

}
