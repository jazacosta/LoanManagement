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
        return await _context.ApprovedLoans
        .Include(x => x.Customer)
        .Include(x => x.Installments)
        .Include(x => x.TermInterestRate)
        .FirstOrDefaultAsync(l => l.Id == loanId, cancellationToken);
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
        using var transaction =  await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await _context.ApprovedLoans.AddAsync(approvedLoan, cancellationToken);
            await _context.Installments.AddRangeAsync(approvedLoan.Installments, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw new InvalidOperationException("An error has occurred while trying to save the Loan and its Installments.", ex);
        }
    }

    public async Task UpdateLoanRequest(LoanRequest loanRequest)
    {
        _context.LoanRequests.Update(loanRequest);
        await _context.SaveChangesAsync();
    }
}
