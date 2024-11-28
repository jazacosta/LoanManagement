using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Infrastructure.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly ApplicationDbContext _context;

    public LoanRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApprovedLoan> GetApprovedLoanById(int loanId, CancellationToken cancellationToken = default)
    {
        var approvedLoan = await _context.ApprovedLoans
                .Include(x => x.Customer)
                .Include(x => x.Installments)
                .FirstOrDefaultAsync(l => l.Id == loanId, cancellationToken);
        return approvedLoan;
    }

    public async Task<LoanRequest> GetLoanRequestById(int id)
    {
        var loanRequest = await _context.LoanRequests
                .FindAsync(id);

        if (loanRequest == null)
            throw new KeyNotFoundException($" The Loan request with Id {id} was not found.");

        return loanRequest;
    }

    public async Task<TermInterestRate> GetTerm(int termId)
    {
        var term = await _context.TermInterestRates.FirstOrDefaultAsync(x => x.Id == termId);
        return term!;
    }

    public async Task SaveApprovedLoan(ApprovedLoan approvedLoan)
    {
        _context.ApprovedLoans.Add(approvedLoan);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateLoanRequest(LoanRequest loanRequest)
    {
        _context.LoanRequests.Update(loanRequest);
        await _context.SaveChangesAsync();
    }
}
