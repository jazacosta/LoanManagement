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

    public async Task<ApprovedLoan> GetApprovedLoanById(int loanId, CancellationToken cancellationToken = default)
    {
        var approvedLoan = await _context.ApprovedLoans
                .Include(x => x.Customer)
                .Include(x => x.Installments)
                .Include(x => x.TermInterestRate)
                .FirstOrDefaultAsync(l => l.Id == loanId, cancellationToken);
        return approvedLoan;
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

    public async Task SaveApprovedLoan(ApprovedLoan approvedLoan, CancellationToken cancellationToken)
    {
            _context.ApprovedLoans.Add(approvedLoan);
            _context.Installments.AddRange(approvedLoan.Installments);
            await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateLoanRequest(LoanRequest loanRequest)
    {
        _context.LoanRequests.Update(loanRequest);
        await _context.SaveChangesAsync();
    }
}
