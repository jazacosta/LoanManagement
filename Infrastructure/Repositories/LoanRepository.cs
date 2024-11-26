using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly ApplicationDbContext _context;

    public LoanRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<LoanRequest> GetLoanRequestById(int Id)
    {
        var loanRequest = await _context.LoanRequests
                .FirstOrDefaultAsync(x => x.Id == Id && x.Status == "Pending Approval");

        if (loanRequest == null)
            throw new KeyNotFoundException($" The Loan request with Id {Id} was not found.");

        return loanRequest;
    }

    public async Task<TermInterestRate> GetTerm(int termId)
    {
        var term = await _context.TermInterestRates.FirstOrDefaultAsync(x => x.Id == termId);
        return term!;
    }

    public async Task SaveApprovedLoan(ApprovedLoan approvedLoan)
    {
        await _context.ApprovedLoans.AddAsync(approvedLoan);
        await _context.SaveChangesAsync();
    }

    public async Task SaveInstallments(List<Installment> installments)
    {
        await _context.Installments.AddRangeAsync(installments);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateLoanRequest(LoanRequest loanRequest)
    {
        _context.LoanRequests.Update(loanRequest);
        await _context.SaveChangesAsync();
    }
}
